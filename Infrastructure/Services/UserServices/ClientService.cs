using Application.Exceptions;
using Application.InfoObjects;
using Application.Services;
using Application.Services.UserServices;
using DataLayer.Dtos;
using DataLayer.Repositories;
using Domain.Entities;
using Domain.Entities.Users;

namespace Infrastructure.Services.UserServices;

public class ClientService : UserServiceBase<Client>, IClientService
{
    private IRepos<Product, ProductDto>? _productRepos;
    private IRepos<Order, OrderDto>? _orderRepos;
    private IRepos<Pharmacy, PharmacyDto>? _pharmacyRepos;
    private IOrderService? _orderService;

    public ClientService(
        Client client, IRepos<Product, ProductDto> productRepos, 
        IRepos<Order, OrderDto> orderRepos, IRepos<Pharmacy, PharmacyDto> pharmacyRepos,
        IRepos<RegisteredUser, UserDto> userRepos, IOrderService orderService
        ) : base(client, userRepos)
    {
        _productRepos = productRepos;
        _orderRepos = orderRepos;
        _pharmacyRepos = pharmacyRepos;
        _orderService = orderService;
    }
    
    public override void Exit()
    {
        _productRepos = null;
        _orderRepos = null;
        _pharmacyRepos = null;
        _orderService = null;
        
        base.Exit();
    }

    IEnumerable<ProductInfo> IRetrieving<ProductInfo>.Get()
    {
        ValidateSession();
        return _productRepos!.FindAll(p => p.IsOnSale)
            .Select(x => new ProductInfo(x.Id, x.FullName, x.Manufacturer, x.Description, x.IsOnSale, x.MedicineInfo == null 
                ? null 
                : new ProductMedicineInfo(
                    x.MedicineInfo.Medicine.Id, x.MedicineInfo.Dosage, x.MedicineInfo.Quantity, x.MedicineInfo.DosageForm, x.MedicineInfo.QuantityForm, x.MedicineInfo.ConsumptionForm
                ), x.Price
                )
            );
    }

    
    public OrderInfo Order(
        IDictionary<uint, uint> orderedProductsIdQuantity, uint deliveryPharmacyId, 
        uint? prescriptionId = null, decimal balanceMoneyToWithdraw = 0
        )
    {
        ValidateSession();
        var res = _orderService!.Order(orderedProductsIdQuantity, deliveryPharmacyId, prescriptionId, User!.Id, balanceMoneyToWithdraw);
        
        User.Balance += Math.Round(res.TotalPrice / 100, 2, MidpointRounding.ToZero);

        return res;
    }

    public decimal GetBalance()
    {
        ValidateSession();
        return User!.Balance;
    }

    IEnumerable<(string, uint)> IRetrieving<(string, uint)>.Get()
    {
        ValidateSession();
        return _pharmacyRepos!.Select(p => (p.Address, p.Id));
    }

    IEnumerable<OrderInfo> IRetrieving<OrderInfo>.Get()
    {
        ValidateSession();
        return _orderRepos!.FindAll(o => o.Client == User)
            .Select(o => new OrderInfo(
                o.Id, o.Client!.Id, o.Status, o.OrderDate, 
                o.Ordered.ToDictionary(p => p.Key.FullName, p => p.Value),
                o.TotalPrice, o.DeliveryPharmacy.Id, o.PrescriptionRequired
                )
            );
    }

    protected override void ValidateSession()
    {
        if (
            _orderRepos == null || _pharmacyRepos == null || 
            _orderService == null || _productRepos == null
            )
        {
            throw new InvalidSessionException("Invalid session of client service.");
        }
        
        base.ValidateSession();
    }
}