using Application.Enums;
using Application.Exceptions;
using Application.InfoObjects;
using Application.Services;
using Application.Services.UserServices;
using DataLayer.Dtos;
using DataLayer.Repositories;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Users;

namespace Infrastructure.Services.UserServices;

public class PharmacistService : UserServiceBase<Pharmacist>, IPharmacistService
{
    private Pharmacy? _pharmacy;
    private IRepos<Product, ProductDto>? _productRepos;
    private IRepos<Order, OrderDto>? _orderRepos;
    private IRepos<Prescription, PrescriptionDto>? _prescriptionRepos;
    private IOrderService? _orderService;

    public PharmacistService(
        Pharmacist pharmacist, IRepos<Product, ProductDto> productRepos,
        IRepos<Order, OrderDto> orderRepos, IRepos<Pharmacy, PharmacyDto> pharmacyRepos,
        IRepos<Prescription, PrescriptionDto> prescriptionRepos,
        IRepos<RegisteredUser, UserDto> userRepos, IOrderService? orderService
    ) : base(pharmacist, userRepos)
    {
        _productRepos = productRepos;
        _orderRepos = orderRepos;
        _pharmacy = pharmacyRepos.Find(ph => ph.WorkingPharmacist == User);
        _prescriptionRepos = prescriptionRepos;
        _orderService = orderService;
    }


    public override void Exit()
    {
        _productRepos = null;
        _orderRepos = null;
        _pharmacy = null;
        _prescriptionRepos = null;
        _orderService = null;
        
        base.Exit();
    }

    public IEnumerable<OrderInfo> Get()
    {
        ValidateSession();

        return _orderRepos!.FindAll(
                o => o.DeliveryPharmacy == _pharmacy
                     && o.Status is OrderStatus.Delivered or OrderStatus.Delivering
            )
            .Select(o => new OrderInfo(
                    o.Id, o.Client?.Id, o.Status, o.OrderDate,
                    o.Ordered.ToDictionary(p => p.Key.Name, p => p.Value),
                    o.TotalPrice, o.DeliveryPharmacy.Id, o.PrescriptionRequired
                )
            );
    }

    public SellResult Sell(
        IDictionary<uint, uint> productsIds, IPaymentService paymentService,
        uint? prescriptionId = null, uint? clientId = null,
        decimal? balanceMoneyToWithdraw = null
    )
    {
        ValidateSession();

        var productIdsArr = productsIds.ToArray();

        ValidateProductList(productIdsArr);

        var productsToSell = FetchProductDetails(productIdsArr);

        var totalPrice = CalculateTotalPrice(productsToSell);

        var prescriptionRequired = IsPrescriptionRequired(productsToSell);

        var prescription = FetchPrescriptionDetails(prescriptionId);

        var prescriptionValid = IsPrescriptionValid(prescription, productsToSell);

        var client = FetchClientDetails(clientId, balanceMoneyToWithdraw);

        var result = DetermineSellResult(client, prescription, prescriptionRequired, prescriptionValid,
            balanceMoneyToWithdraw, totalPrice, paymentService);

        if (result != SellResult.Ok) return result;
        
        if (client != null)
        {
            client.Balance += Math.Round(totalPrice / 100, 2, MidpointRounding.ToZero);
        }

        RemoveProductsFromStock(productsToSell);

        return result;
    }

    public IDictionary<ProductInfo, uint> GetPharmacyProducts()
    {
        ValidateSession();
        return _pharmacy!.Stock.ProductQuantity.ToDictionary(
            i => new ProductInfo(
                i.Key.Id, i.Key.Name, i.Key.Manufacturer, i.Key.Description, i.Key.IsOnSale, i.Key.MedicineInfo == null 
                    ? null : new ProductMedicineInfo(
                        i.Key.MedicineInfo.Medicine.Id, i.Key.MedicineInfo.Dosage, i.Key.MedicineInfo.Quantity, 
                        i.Key.MedicineInfo.DosageForm, i.Key.MedicineInfo.QuantityForm, i.Key.MedicineInfo.ConsumptionForm
                ), 
                i.Key.Price
            ), 
            i => i.Value
        );
    }

    protected override void ValidateSession()
    {
        if (_productRepos == null || _prescriptionRepos == null || _pharmacy == null)
        {
            throw new InvalidSessionException("Invalid session of pharmacist service");
        }
        
        base.ValidateSession();
    }

    private static void ValidateProductList(IEnumerable<KeyValuePair<uint, uint>> productIdsArr)
    {
        if (!productIdsArr.Any())
        {
            throw new ArgumentException("Cannot sell empty check.");
        }
    }

    private IDictionary<Product, uint> FetchProductDetails(IEnumerable<KeyValuePair<uint, uint>> productIdsArr)
    {
        return productIdsArr
            .ToDictionary(pIdQuantity => _productRepos!.Find(pIdQuantity.Key)
                                         ?? throw new ArgumentException(
                                             $"Provided product to sell of id {pIdQuantity} was not found."),
                pIdQuantity => pIdQuantity.Value
            );
    }

    private static decimal CalculateTotalPrice(IDictionary<Product, uint> productsToSell)
    {
        return productsToSell.Keys.Sum(p => p.Price * productsToSell[p]);
    }

    private static bool IsPrescriptionRequired(IDictionary<Product, uint> productsToSell)
    {
        return productsToSell.Any(p => p.Key.MedicineInfo is { Medicine.IsPrescriptionRequired: true });
    }

    private Prescription? FetchPrescriptionDetails(uint? prescriptionId)
    {
        if (prescriptionId == null)
        {
            return null;
        }

        return _prescriptionRepos!.Find(prescriptionId.Value)
               ?? throw new ArgumentException($"Provided prescription of id {prescriptionId} was not found.");
    }

    private static bool? IsPrescriptionValid(Prescription? prescription, IDictionary<Product, uint> productsToSell)
    {
        return prescription == null
            ? null
            : productsToSell.Keys.Where(p => p.MedicineInfo is { Medicine.IsPrescriptionRequired: true })
                .All(p => prescription.Prescribed.Contains(p.MedicineInfo!.Medicine));
    }

    private Client? FetchClientDetails(uint? clientId, decimal? balanceMoneyToWithdraw)
    {
        if (clientId != null)
        {
            return UserRepos!.Find(clientId.Value) as Client
                   ?? throw new ArgumentException($"Provided client of id {clientId} was not found");
        }

        if (balanceMoneyToWithdraw != null)
        {
            throw new ArgumentException(
                "Unable to withdraw money from client balance, because client id was not provided");
        }

        return null;
    }

    private static SellResult DetermineSellResult(Client? client, Prescription? prescription, bool prescriptionRequired,
        bool? prescriptionValid, decimal? balanceMoneyToWithdraw, decimal totalPrice, IPaymentService paymentService)
    {
        return (client, prescription, prescriptionRequired, prescriptionValid, balanceMoneyToWithdraw) switch
        {
            (_, null, true, _, _) => SellResult.PrescriptionRequired,
            (_, not null, true, false, _) => SellResult.PrescriptionRequired,
            (null, _, _, _, not null) => throw new ArgumentException(
                "Unable to withdraw money from client balance, because client id was not provided"),
            (_, _, _, _, null) => paymentService.Pay(totalPrice) ? SellResult.Ok : SellResult.PaymentFailed,
            (_, _, _, _, not null) => paymentService.Pay(totalPrice - balanceMoneyToWithdraw.Value)
                ? SellResult.Ok
                : SellResult.PaymentFailed
        };
    }

    private void RemoveProductsFromStock(IDictionary<Product, uint> productsToSell)
    {
        foreach (var kvp in productsToSell)
        {
            _pharmacy!.Stock.Remove(kvp.Key, kvp.Value);
        }
    }



    // public SellResult Sell(
    //     IDictionary<uint, uint> productsIds, IPaymentService paymentService, 
    //     uint? prescriptionId = null, uint? clientId = null, 
    //     uint? balanceMoneyToWithdraw = null
    // )
    // {
    //     if (_productRepos == null || _prescriptionRepos == null 
    //         || _usersRepos == null || _pharmacy == null 
    //         || _pharmacist == null 
    //         )
    //     {
    //         throw new InvalidSessionException("Invalid session of pharmacist service");
    //     }
    //
    //     var productIdsArr = productsIds.ToArray();
    //     
    //     
    //     if (productIdsArr.Any())
    //     {
    //         throw new ArgumentException("Cannot sell empty check.");
    //     }
    //
    //     
    //     var productsToSell = productIdsArr
    //         .ToDictionary(pIdQuantity => _productRepos.Find(pIdQuantity.Key) 
    //                        ?? throw new ArgumentException($"Provided product to sell of id {pIdQuantity} was not found."),
    //                         pIdQuantity => pIdQuantity.Value
    //         );
    //
    //     
    //     var totalPrice = (uint)productsToSell.Keys.Sum(p => p.Price * productsToSell[p]);
    //
    //     
    //     var prescriptionRequired = 
    //         productsToSell.Any(p => p.Key is MedicineProduct { Medicine.IsPrescriptionRequired: true });
    //
    //     var prescription = prescriptionId == null 
    //         ? null 
    //         : _prescriptionRepos.Find(prescriptionId.Value) 
    //           ?? throw new ArgumentException($"Provided prescription of id {prescriptionId} was not found.");
    //
    //     bool? prescriptionValid = prescription == null
    //         ? null
    //         : productsToSell.Keys.Where(p => p is MedicineProduct { Medicine.IsPrescriptionRequired: true }).Cast<MedicineProduct>()
    //                   .All(mp => prescription.Prescribed.Contains(mp.Medicine));
    //     
    //     var client = clientId == null
    //         ? null
    //         : _usersRepos.Find(clientId.Value) as Client
    //           ?? throw new ArgumentException($"Provided client of id {clientId} was not found");
    //
    //     
    //     var result = (client, prescription, prescriptionRequired, prescriptionValid, balanceMoneyToWithdraw) switch
    //     {
    //         (_, null, true, _, _) => SellResult.PrescriptionRequired,
    //         (_, not null, true, false, _) => SellResult.PrescriptionRequired,
    //         
    //         (null, _, _, _, not null) 
    //             => throw new ArgumentException("Unable to withdraw money from client balance, because client id was not provided"),
    //         
    //         (_, _, _, _, null) => paymentService.Pay(totalPrice) ? SellResult.Ok : SellResult.PaymentFailed,
    //         
    //         (_, _, _, _, not null) 
    //             => paymentService.Pay(totalPrice - balanceMoneyToWithdraw.Value) 
    //                 ? SellResult.Ok 
    //                 : SellResult.PaymentFailed
    //     };
    //
    //     if (result != SellResult.Ok) return result;
    //     
    //     foreach (var kvp in productsToSell)
    //     {
    //         _pharmacy.Stock.Remove(kvp.Key, kvp.Value);
    //     }
    //     
    //     return result;
    // }

    public OrderGiveOutResult GiveOutOrder(uint orderId, uint? prescriptionId = null)
    {
        ValidateSession();

        var prescription = prescriptionId == null
            ? null
            : _prescriptionRepos!.Find(prescriptionId.Value);

        if (!_pharmacy!.Stock.HasOrder(orderId))
        {
            return OrderGiveOutResult.OrderNotFound;
        }
        
        var order = _orderRepos!.Find(orderId);


        if (prescription == null && order!.PrescriptionRequired)
        {
            return OrderGiveOutResult.PrescriptionRequired;
        }

        if (
            order!.PrescriptionRequired && IsPrescriptionValid(
            prescription, order.Ordered.ToDictionary(o => o.Key, o => o.Value)
            ) == true
        )
        {
            return OrderGiveOutResult.PrescriptionRequired;
        }

        _pharmacy!.Stock.RemoveOrder(orderId);
        order.Status = OrderStatus.PickedUp;

        return OrderGiveOutResult.Success;
    }

    public void RegisterNewProducts(IEnumerable<(uint productId, uint quantity)> products)
    {
        ValidateSession();
        
        var toRegister = products
            .Select(pIdQ 
                => (_productRepos!.Find(pIdQ.productId) 
                    ?? throw new ArgumentException($"Product to register of id {pIdQ.productId} was not found."),
                pIdQ.quantity)
            );

        foreach (var (product, quantity) in toRegister)
        {
            _pharmacy!.Stock.Add(product, quantity);
        }
    }

    public void AcceptOrder(uint orderId)
    {
        ValidateSession();
        
        var order = _orderRepos!.Find(o 
            => o.Id == orderId && o.DeliveryPharmacy == _pharmacy && 
               o.Status == OrderStatus.Delivering) ?? throw new ArgumentException($"Order of id {orderId} was not found delivering to pharmacy of id {_pharmacy!.Id}");

        _pharmacy!.Stock.AddOrder(order);
        order.Status = OrderStatus.Delivered;
    }

    // IEnumerable<(ProductInfo, uint)> IRetrieving<(ProductInfo, uint)>.Get()
    // {
    //     ValidateSession();
    //     return _pharmacy!.Stock.ProductQuantity
    //         .Where(i => i.Key.IsOnSale)
    //         .Select(i => (new ProductInfo(
    //                 i.Key.Id, i.Key.Name, i.Key.Manufacturer, i.Key.Description, i.Key.IsOnSale, 
    //                 i.Key.MedicineInfo == null 
    //                     ? null 
    //                     : new ProductMedicineInfo(
    //                         i.Key.MedicineInfo.Medicine.Id, i.Key.MedicineInfo.Dosage, 
    //                         i.Key.MedicineInfo.Quantity, i.Key.MedicineInfo.DosageForm, 
    //                         i.Key.MedicineInfo.QuantityForm, i.Key.MedicineInfo.ConsumptionForm),
    //                 i.Key.Price
    //                 ), 
    //             i.Value)
    //         );
    // }
}