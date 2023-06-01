using Application.Enums;
using Application.InfoObjects;
using DataLayer.Dtos;
using Domain.Entities;

namespace Application.Services.UserServices;

public interface IPharmacistService : IUserService, IRetrieving<OrderInfo>
{
    public SellResult Sell(
        IDictionary<uint, uint> productsIds, IPaymentService paymentService,
        uint? prescriptionId = null, uint? clientId = null,
        decimal? balanceMoneyToWithdraw = null
    );


    public IDictionary<ProductInfo, uint> GetPharmacyProducts();

    public OrderGiveOutResult GiveOutOrder(uint orderId, uint? prescriptionId = null);

    public void RegisterNewProducts(IEnumerable<(uint productId, uint quantity)> products);

    public void AcceptOrder(uint orderId);
}