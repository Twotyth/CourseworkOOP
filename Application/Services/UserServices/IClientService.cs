using Application.InfoObjects;
using DataLayer.Dtos;

namespace Application.Services.UserServices;

public interface IClientService : IUserService, IRetrieving<OrderInfo>, IRetrieving<ProductInfo>, IRetrieving<(string, uint)>
{
    public OrderInfo Order(
        IDictionary<uint, uint> orderedProductsIdQuantity, uint deliveryPharmacyId,
        uint? prescriptionId = null, decimal balanceMoneyToWithdraw = 0
    );
    
    public decimal GetBalance();
}