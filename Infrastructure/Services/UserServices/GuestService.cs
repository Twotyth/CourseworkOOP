using Application.InfoObjects;
using Application.Services;
using Application.Services.UserServices;
using DataLayer.Dtos;
using DataLayer.Repositories;
using Domain.Entities;
using Domain.Entities.Users;

namespace Infrastructure.Services.UserServices;

public class GuestService : IGuestService
{
    private readonly IRepos<Product, ProductDto> _productRepos;
    private readonly IRepos<Order, OrderDto> _orderRepos;
    private readonly IRepos<Pharmacy, PharmacyDto> _pharmacyRepos;
    private readonly IRepos<Prescription, PrescriptionDto> _prescriptionRepos;
    private readonly IRepos<RegisteredUser, UserDto> _userRepos;
    private readonly IRepos<Medicine, MedicineDto> _medicineRepos;
    private readonly IOrderService _orderService;

    public GuestService(
        IRepos<Product, ProductDto> productRepos, IRepos<Order, OrderDto> orderRepos,
        IRepos<Prescription, PrescriptionDto> prescriptionRepos, IRepos<Pharmacy, PharmacyDto> pharmacyRepos, 
        IRepos<RegisteredUser, UserDto> userRepos, IRepos<Medicine, MedicineDto> medicineRepos, IOrderService orderService
        )
    {
        _productRepos = productRepos;
        _orderRepos = orderRepos;
        _pharmacyRepos = pharmacyRepos;
        _userRepos = userRepos;
        _medicineRepos = medicineRepos;
        _orderService = orderService;
        _prescriptionRepos = prescriptionRepos;
    }

    IEnumerable<ProductInfo> IRetrieving<ProductInfo>.Get()
        => _productRepos.FindAll(p => p.IsOnSale)
            .Select(x => new ProductInfo(x.Id, x.FullName, x.Manufacturer, x.Description, x.IsOnSale, x.MedicineInfo == null 
                ? null 
                : new ProductMedicineInfo(
                    x.MedicineInfo.Medicine.Id, x.MedicineInfo.Dosage, 
                    x.MedicineInfo.Quantity, x.MedicineInfo.DosageForm, 
                    x.MedicineInfo.QuantityForm, x.MedicineInfo.ConsumptionForm
                ), 
                    x.Price
                )
            );

    IEnumerable<(string, uint)> IRetrieving<(string, uint)>.Get() 
        => _pharmacyRepos.Select(x => (x.Address, x.Id));


    public OrderInfo Order(
        IDictionary<uint, uint> orderedProductsIdQuantity, uint deliveryPharmacyId,
        uint? prescriptionId = null
        )
    {
        return _orderService.Order(orderedProductsIdQuantity, deliveryPharmacyId, prescriptionId);
    }

    public IUserService Register(string login, string password)
    {
        _userRepos.Add(new ClientDto(0, login, password, 0));
        
        return Login(login, password);
    }
 
    public IUserService Login(string login, string password)
    {
        var user = _userRepos.Find(user => user.Login == login && user.Password == password) 
                   ?? throw new ArgumentException("Invalid login credentials.");

        return user switch
        {
            Client client => new ClientService(
                client, _productRepos, _orderRepos, 
                _pharmacyRepos, _userRepos, _orderService
            ),
            
            Pharmacist pharmacist => new PharmacistService(
                pharmacist, _productRepos, _orderRepos, _pharmacyRepos, 
                _prescriptionRepos, _userRepos, _orderService
                ),
            
            Admin admin => new AdminService(
                admin, _productRepos, _orderRepos, _pharmacyRepos, _userRepos, _medicineRepos
                ),
            
            _ => throw new ArgumentOutOfRangeException($"Unexpected Exception: user role {user.GetType()} was not expected")
        };
    }
}