using System.Collections.Immutable;
using System.IO.Compression;
using System.Text;
using Application.Enums;
using Application.Exceptions;
using Application.InfoObjects;
using Application.Services;
using Application.Services.UserServices;
using DataLayer.Dtos;
using DataLayer.Exceptions;
using DataLayer.Repositories;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Users;
using MedicineInfo = Application.InfoObjects.MedicineInfo;

namespace Infrastructure.Services.UserServices;

public class AdminService : UserServiceBase<Admin>, IAdminService
{
    private IRepos<Product, ProductDto>? _productRepos;
    private IRepos<Order, OrderDto>? _orderRepos;
    private IRepos<Pharmacy, PharmacyDto>? _pharmacyRepos;
    private IRepos<Medicine, MedicineDto>? _medicineRepos;

    public AdminService
    (
        Admin admin, IRepos<Product, ProductDto> productRepos, 
        IRepos<Order, OrderDto> orderRepos, IRepos<Pharmacy, PharmacyDto> pharmacyRepos, 
        IRepos<RegisteredUser, UserDto> userRepos, IRepos<Medicine, MedicineDto> medicineRepos) 
        : base(admin, userRepos)
    {
        _productRepos = productRepos;
        _orderRepos = orderRepos;
        _pharmacyRepos = pharmacyRepos;
        _medicineRepos = medicineRepos;
    }

    public override void Exit()
    {
        _productRepos = null;
        _orderRepos = null;
        _pharmacyRepos = null;
        _medicineRepos = null;
        
        base.Exit();
    }
    
    protected override void ValidateSession()
    {
        if (_productRepos == null || _pharmacyRepos == null || _orderRepos == null || _medicineRepos == null)
        {
            throw new InvalidSessionException("Invalid session of admin service");
        }
        
        base.ValidateSession();
    }

    IEnumerable<ProductInfo> IRetrieving<ProductInfo>.Get()
    {
        ValidateSession();
        
        return _productRepos!.FindAll(_ => true).Select(p
            => new ProductInfo(p.Id, p.Name, p.Manufacturer, p.Description, p.IsOnSale, p.MedicineInfo == null 
                ? null 
                : new ProductMedicineInfo(
                    p.MedicineInfo.Medicine.Id, p.MedicineInfo.Dosage, p.MedicineInfo.Quantity, p.MedicineInfo.DosageForm, p.MedicineInfo.QuantityForm, p.MedicineInfo.ConsumptionForm
                ), 
            p.Price
            )
        );
    }

    IEnumerable<ClientInfo> IRetrieving<ClientInfo>.Get()
    {
        ValidateSession();

        return UserRepos!.FindAll(user => user is Client).Cast<Client>()
            .Select(client => new ClientInfo(client.Id, client.Login, client.Balance)
        );
    }

    IEnumerable<OrderInfo> IRetrieving<OrderInfo>.Get()
    {
        ValidateSession();

        return _orderRepos!.Select(
            o => new OrderInfo(
                o.Id, o.Client?.Id, o.Status, o.OrderDate, 
                o.Ordered.ToDictionary(p => p.Key.Name, q => q.Value),
                o.TotalPrice,o.DeliveryPharmacy.Id, o.PrescriptionRequired
            )
        );
    }
    
    IEnumerable<MedicineInfo> IRetrieving<MedicineInfo>.Get()
    {
        ValidateSession();

        return _medicineRepos!.Select(med => new MedicineInfo(med.Id, med.Name, med.IsPrescriptionRequired));
    }

    IEnumerable<PharmacistInfo> IRetrieving<PharmacistInfo>.Get()
    {
        ValidateSession();

        return UserRepos!.FindAll(user => user is Pharmacist).Cast<Pharmacist>()
            .Select(
                ph => new PharmacistInfo(ph.Id, ph.Login, ph.Salary)
            );
    }

    IEnumerable<PharmacyInfo> IRetrieving<PharmacyInfo>.Get()
    {
        ValidateSession();

        return _pharmacyRepos!.Select(pharmacy =>
            new PharmacyInfo(pharmacy.Id, pharmacy.Address, pharmacy.WorkingPharmacist?.Id)
        );
    }

    public DeleteUserResult DeleteUser(uint userId)
    {
        ValidateSession();

        var user = UserRepos!.Find(user => user is not Admin && user.Id == userId);

        if (user == null) return DeleteUserResult.UserNotFound;

        switch (user)
        {
            case Client client:
            {
                foreach (var order in _orderRepos!.FindAll(o => o.Client == client))
                {
                    order.Client = null;
                }

                break;
            }
            case Pharmacist pharmacist:
            {
                var pharmacy = _pharmacyRepos!.Find(ph => ph.WorkingPharmacist == pharmacist);

                if (pharmacy != null)
                {
                    pharmacy.WorkingPharmacist = null;
                }

                break;
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(userId));
        }
        
        UserRepos.Delete(user);
        return DeleteUserResult.Success;
    }

    public DeleteProductResult DeleteProduct(uint productId)
    {
        ValidateSession();
        
        var product = _productRepos!.Find(productId);

        if (product == null) return DeleteProductResult.ProductNotFound;

        try
        {
            _productRepos.Delete(product);
        }
        catch (OperationCanceledException)
        {
            return DeleteProductResult.NotDeliveringRequired;
        }

        return DeleteProductResult.Success;
    }

    public WithdrawFromSaleResult WithdrawFromSale(uint productId)
    {
        ValidateSession();
        var product = _productRepos!.Find(productId);
        
        if (product == null) return WithdrawFromSaleResult.ProductNotFound;

        if (!product.IsOnSale) return WithdrawFromSaleResult.ProductAlreadyWithdrawn;
        
        product.IsOnSale = false;

        return WithdrawFromSaleResult.Success;
    }

    public ReturnToSaleResult ReturnToSale(uint productId)
    {
        ValidateSession();
        var product = _productRepos!.Find(productId);

        if (product == null) return ReturnToSaleResult.ProductNotFound;

        if (product.IsOnSale) return ReturnToSaleResult.ProductNotWithdrawn;

        product.IsOnSale = true;

        return ReturnToSaleResult.Success;
    }

    public DeletePharmacyResult DeletePharmacy(uint pharmacyId)
    {
        ValidateSession();

        var pharmacy = _pharmacyRepos!.Find(ph => ph.Id == pharmacyId);

        if (pharmacy == null) return DeletePharmacyResult.PharmacyNotFound;

        foreach (
            var order in _orderRepos!.FindAll(
                o => o.DeliveryPharmacy == pharmacy && o.Status == OrderStatus.Delivering
                )
        )
        {
            CancelOrder(order.Id);
        }
        
        _pharmacyRepos.Delete(pharmacy);
        
        return DeletePharmacyResult.Success;
    }

    public EditProductResult EditProduct(
        uint productId, string newName, string newManufacturer,
        string newDescription,
        ProductMedicineInfo? newMedicineInfo, decimal newPrice
        )
    {
        ValidateSession();
        
        var product = _productRepos!.Find(productId);

        if (product == null) return EditProductResult.ProductNotFound;

        Medicine? newMedicine = null;
        
        if (newMedicineInfo != null)
        {
            newMedicine = _medicineRepos!.Find(newMedicineInfo.MedicineId);

            if (newMedicine == null) return EditProductResult.NewMedicineNotFound;
        }

        try
        {
            if (product.Name != newName)
            {
                product.Name = newName;
            }

            if (product.Manufacturer != newManufacturer)
            {
                product.Manufacturer = newManufacturer;
            }

            if (product.Description != newDescription)
            {
                product.Description = newDescription;
            }

            if (newMedicineInfo != null)
            {
                product.MedicineInfo = new Domain.Entities.MedicineInfo(
                    newMedicine!, newMedicineInfo.Dosage, newMedicineInfo.Quantity,
                    newMedicineInfo.DosageForm, newMedicineInfo.QuantityForm, 
                    newMedicineInfo.ConsumptionForm
                );
            }
            else
            {
                product.MedicineInfo = null;
            }

            if (product.Price != newPrice)
            {
                product.Price = newPrice;
            }
        }
        catch (ArgumentException)
        {
            return EditProductResult.NewValueDoesNotMeetReqs;
        }
        
        return EditProductResult.Success;
    }

    public EditPharmacyResult EditPharmacy(uint pharmacyId, string newAddress, uint? newWorkingPharmacistId)
    {
        ValidateSession();

        var pharmacy = _pharmacyRepos!.Find(pharmacyId);

        if (pharmacy == null) return EditPharmacyResult.PharmacyNotFound;

        var pharmacist = newWorkingPharmacistId == null
            ? null
            : UserRepos!.Find(newWorkingPharmacistId.Value) as Pharmacist;

        if (newWorkingPharmacistId == null && pharmacist == null)
        {
            return EditPharmacyResult.NewValueDoesNotMeetReqs;
        }
        
        try
        {
            pharmacy.Address = newAddress;
            pharmacy.WorkingPharmacist = pharmacist;
        }
        catch (ArgumentException)
        {
            return EditPharmacyResult.NewValueDoesNotMeetReqs;
        }
        
        return EditPharmacyResult.Success;
    }

    public EditPharmacistResult EditPharmacist(uint pharmacistId, uint newSalary)
    {
        ValidateSession();

        if (UserRepos!.Find(u => u.Id == pharmacistId) is not Pharmacist pharmacist)
        {
            return EditPharmacistResult.PharmacistNotFount;
        }

        pharmacist.Salary = newSalary;
        
        return EditPharmacistResult.Success;
    }

    public CancelOrderResult CancelOrder(uint orderId)
    {
        ValidateSession();

        var order = _orderRepos!.Find(o => o.Id == orderId && o.Status == OrderStatus.Delivering);

        if (order == null) return CancelOrderResult.DeliveringOrderNotFound;

        order.Status = OrderStatus.Canceled;
        
        return CancelOrderResult.Success;
    }

    public AddUserResult AddUser(UserDto userInfo)
    {
        ValidateSession();

        try
        {
            UserRepos!.Add(userInfo);
        }
        catch (ArgumentException)
        {
            return AddUserResult.PropsDoNotMeetReqs;
        }
        catch (DuplicateException)
        {
            return AddUserResult.UserAlreadyExists;
        }

        return AddUserResult.Success;
    }

    public AddProductResult AddProduct(string name, string manufacturer, string description, decimal price, ProductMedicineInfo? medicineInfo)
    {
        ValidateSession();

        try
        {
            _productRepos!.Add(new ProductDto(0, name, manufacturer, description, medicineInfo == null 
                ? null 
                : new MedicineInfoDto(
                    medicineInfo.MedicineId, medicineInfo.Dosage, medicineInfo.Quantity, medicineInfo.DosageForm, medicineInfo.QuantityForm, medicineInfo.ConsumptionForm
                ), 
                price, true
            )
            );
        }
        catch (DuplicateException)
        {
            return AddProductResult.ProductAlreadyExists;
        }
        catch (ArgumentException)
        {
            return AddProductResult.PropsDoNotMeetReqs;
        }

        return AddProductResult.Success;
    }

    public AddPharmacyResult AddPharmacy(string address, uint workingPharmacistId)
    {
        ValidateSession();

        try
        {
            _pharmacyRepos!.Add(
                new PharmacyDto(
                    0, address, ImmutableDictionary<uint, uint>.Empty, 
                    workingPharmacistId, new Dictionary<uint, (DateOnly, decimal, bool)>()
                )
            );
        }
        catch (DuplicateException)
        {
            return AddPharmacyResult.PharmacyAlreadyExists;
        }
        catch (ArgumentException)
        {
            return AddPharmacyResult.PropsDoNotMeetReqs;
        }

        return AddPharmacyResult.Success;
    }

    public bool SaveData(string path, string filename)
    {
        ValidateSession();
        
        try
        {
            using var memoryStream = File.Create(Path.Combine(path, filename + ".savepoint"));
            
            var zip = new ZipArchive(memoryStream, ZipArchiveMode.Create);

            AddEntry("user_repos.json", UserRepos!.Serialize());
            AddEntry("product_repos.json", _productRepos!.Serialize());
            AddEntry("order_repos.json", _orderRepos!.Serialize());
            AddEntry("pharmacy_repos.json", UserRepos!.Serialize());

            void AddEntry(string fileName, string fileContent)
            {
                var entry = zip.CreateEntry(fileName);
                using var stream = entry.Open();
                stream.Write(Encoding.UTF8.GetBytes(fileContent), 0, fileContent.Length);
            }
        }
        catch
        {
            return false;
        }

        return true;
    }

    public bool LoadData(FileStream file)
    {
        ValidateSession();
        
        var prodBackup = _productRepos!;
        var userBackup = UserRepos!;
        var orderBackup = _orderRepos!;
        var pharmacyBackup = _pharmacyRepos!;
        var medicineBackup = _medicineRepos!;
        
        try
        {
            var zip = new ZipArchive(file, ZipArchiveMode.Read);

            if (zip.Entries.All(e => e.Name != @"medicine_repos.json"))
            {
                return false;
            }
            
            if (zip.Entries.All(e => e.Name != "user_repos.json"))
            {
                return false;
            }

            if (zip.Entries.All(e => e.Name != "product_repos.json"))
            {
                return false;
            }
            
            if (zip.Entries.All(e => e.Name != "order_repos.json"))
            {
                return false;
            }
            
            if (zip.Entries.All(e => e.Name != "pharmacy_repos.json"))
            {
                return false;
            }

            if (zip.Entries.Any(
                    e => e.Name is not "medicine_repos.json" and not
                        "user_repos.json" and not "product_repos.json" and not
                        "order_repos.json" and not "pharmacy_repos.json")
               )
            {
                return false;
            }


            LoadEntry("medicine_repos.json");
            Deserialize(_medicineRepos!);

            LoadEntry("user_repos.json");
            Deserialize(UserRepos!);
            
            LoadEntry("product_repos.json");
            _productRepos = new ProductRepos(_medicineRepos!);
            Deserialize(_productRepos);
            
            LoadEntry("pharmacy_repos.json");
            _pharmacyRepos = new PharmacyRepos(UserRepos!, _productRepos);
            Deserialize(_pharmacyRepos);
            
            LoadEntry("order_repos.json");
            _orderRepos = new OrderRepos(_productRepos, _pharmacyRepos, UserRepos!);
            Deserialize(_orderRepos);
            
            void LoadEntry(string name)
            {
                zip.GetEntry(name)!.ExtractToFile("temp.json", true);
            }

            void Deserialize<T, U>(IRepos<T, U> repos)
            {
                repos.Deserialize(File.ReadAllText("temp.json"));
            }
        }
        catch
        {
            _productRepos = prodBackup;
            UserRepos = userBackup;
            _orderRepos = orderBackup;
            _pharmacyRepos = pharmacyBackup;
            _medicineRepos = medicineBackup;

            return false;
        }

        return true;
    }

    public IEnumerable<(string, uint)> Get()
    {
        ValidateSession();
        return _pharmacyRepos!.Select(i => (i.Address, i.Id));
    }
}