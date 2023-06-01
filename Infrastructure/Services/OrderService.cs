using Application.Exceptions;
using Application.InfoObjects;
using Application.Services;
using DataLayer.Dtos;
using DataLayer.Repositories;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Users;

namespace Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IRepos<Product, ProductDto> _productRepos;
    private readonly IRepos<RegisteredUser, UserDto> _userRepos;
    private readonly IRepos<Order, OrderDto> _orderRepos;
    private readonly IRepos<Prescription, PrescriptionDto> _prescriptionRepos;

    public OrderService(IRepos<Product, ProductDto> productRepos, IRepos<RegisteredUser, UserDto> userRepos, IRepos<Order, OrderDto> orderRepos, IRepos<Prescription, PrescriptionDto> prescriptionRepos)
    {
        _productRepos = productRepos;
        _userRepos = userRepos;
        _orderRepos = orderRepos;
        _prescriptionRepos = prescriptionRepos;
    }

    public OrderInfo Order(
        IDictionary<uint, uint> orderedProductsIdQuantity, uint deliveryPharmacyId, 
        uint? prescriptionId = null, uint? clientId = null, decimal balanceMoneyToWithdraw = 0)
    {
        if (!orderedProductsIdQuantity.Any())
        {
            throw new ArgumentException("Cannot order empty product list.");
        }
        
        var orderedProducts = orderedProductsIdQuantity
            .Select(pIdQuantity => _productRepos.Find(pIdQuantity.Key) 
                           ?? throw new ArgumentException($"Product to order of id {pIdQuantity} was not found.")
            ).ToArray();
        
        
        if (orderedProducts.Any(p => !p.IsOnSale))
        {
            throw new ArgumentException("Products to order had products that are not on sale. Please remove them from order and try again.");
        }
        

        RegisteredUser? user = null;
        var prescription = prescriptionId == null 
            ? null 
            : _prescriptionRepos.Find(prescriptionId.Value);
        
        if (clientId != null)
        {
            user = _userRepos.Find(clientId.Value) 
                   ?? throw new ArgumentException($"Provided client id of {clientId} was not found.");
        }

        if (clientId != null && user is not Client)
        {
            throw new ArgumentException($"Provided client id of {clientId} is not id of pharmacy chain client.");
        }
        
        
        var prescriptionNeeded = orderedProducts.All(product =>
        {
            if (product.MedicineInfo == null) return false;

            if (!product.MedicineInfo.Medicine.IsPrescriptionRequired) return false;

            return prescription == null || prescription.Prescribed.Contains(product.MedicineInfo.Medicine);
        });

        var res = new OrderDto(
            null, user?.Id, OrderStatus.Delivering, DateOnly.FromDateTime(DateTime.Today), deliveryPharmacyId, 
            orderedProducts.Join(
                orderedProductsIdQuantity, i => i.Price, i => i.Value, 
                (i1, i2) => i1.Price*i2.Value)
                .Sum(i => i) - balanceMoneyToWithdraw, balanceMoneyToWithdraw,
            orderedProductsIdQuantity, prescriptionNeeded
            );
        
        _orderRepos.Add(res);
        
        

        var order = _orderRepos.Find(o =>
                    {
                        var equivalentProductsOrdered = o.Ordered
                            .SequenceEqual(orderedProductsIdQuantity
                                .Select(pIdQuantity =>
                                    new KeyValuePair<Product, uint>(
                                        _productRepos.Find(pIdQuantity.Key) ??
                                        throw new ArgumentException(
                                            $"Cannot find product of id {pIdQuantity.Key} to order to address {deliveryPharmacyId}."),
                                        pIdQuantity.Value
                                    )
                                )
                            );
                        
                        return equivalentProductsOrdered
                               && o.DeliveryPharmacy.Id == deliveryPharmacyId
                               && o.OrderDate == DateOnly.FromDateTime(DateTime.Today)
                               && o.PrescriptionRequired == prescriptionNeeded
                               && o.Status == OrderStatus.Delivering;
                    })
                    ?? throw new ReposException("Order was not added to repository.");

        if (order.Client != null)
        {
            order.Client.Balance -= balanceMoneyToWithdraw;
        }

        return new OrderInfo(
            order.Id, order.Client?.Id, order.Status, order.OrderDate, 
            order.Ordered.ToDictionary(
                p => p.Key.FullName, p => p.Value
            ), order.TotalPrice, order.DeliveryPharmacy.Id, order.PrescriptionRequired
        );
    }
}