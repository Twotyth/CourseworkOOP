using Application.InfoObjects;
using DataLayer.Dtos;
using Domain.Entities;

namespace Application.Services;

public interface IOrderService
{
    public OrderInfo Order(
        IDictionary<uint, uint> orderedProductsIdQuantity, uint deliveryPharmacyId, 
        uint? prescriptionId = null, uint? clientId = null,
        decimal balanceMoneyToWithdraw = 0
    );
}