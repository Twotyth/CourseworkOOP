using Application.InfoObjects;
using DataLayer.Dtos;

namespace Application.Services.UserServices;

public interface IGuestService : IRetrieving<ProductInfo>, IRetrieving<(string, uint)>
{
    public OrderInfo Order(
        IDictionary<uint, uint> orderedProductsIdQuantity, uint deliveryPharmacyId, 
        uint? prescriptionId = null);
    public IUserService Register(string login, string password);
    public IUserService Login(string login, string password);
}

