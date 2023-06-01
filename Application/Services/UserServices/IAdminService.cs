using Application.Enums;
using Application.InfoObjects;
using DataLayer.Dtos;

namespace Application.Services.UserServices;

public interface IAdminService : 
    IUserService, IRetrieving<MedicineInfo>, IRetrieving<ProductInfo>, IRetrieving<ClientInfo>,
    IRetrieving<PharmacistInfo>, IRetrieving<OrderInfo>, IRetrieving<PharmacyInfo>,
    IRetrieving<(string, uint)>
{
    public DeleteUserResult DeleteUser(uint userId);
    public DeleteProductResult DeleteProduct(uint productId);
    public WithdrawFromSaleResult WithdrawFromSale(uint productId);
    public ReturnToSaleResult ReturnToSale(uint productId);
    public DeletePharmacyResult DeletePharmacy(uint pharmacyId);
    public EditProductResult EditProduct(
        uint productId, string newName, string newManufacturer,
        string newDescription,
        ProductMedicineInfo? newMedicineInfo, decimal newPrice
        );
    public EditPharmacyResult EditPharmacy(uint pharmacyId, string newAddress, uint? newWorkingPharmacistId);
    public EditPharmacistResult EditPharmacist(uint pharmacistId, uint newSalary);
    
    public CancelOrderResult CancelOrder(uint orderId);

    public AddUserResult AddUser(UserDto userInfo);
    public AddProductResult AddProduct(string name, string manufacturer, string description, decimal price, ProductMedicineInfo? medicineInfo);
    public AddPharmacyResult AddPharmacy(string address, uint workingPharmacistId);
}


