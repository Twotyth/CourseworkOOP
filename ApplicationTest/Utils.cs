using Application.Services;
using Application.Services.UserServices;
using DataLayer.Dtos;
using DataLayer.Repositories;
using Domain.Entities;
using Domain.Entities.Users;
using Infrastructure.Services;
using Infrastructure.Services.UserServices;

namespace ApplicationTest;

public abstract class Utils
{
    static Utils()
    {
        File.WriteAllText(@".\medicinetest.json", "[]");
        File.WriteAllText(@".\prescriptionstest.json", "[]");
    }

    protected Utils()
    {
        PharmacyRepos = new PharmacyRepos(UserRepos, ProductRepos);
        OrderRepos = new OrderRepos(ProductRepos, PharmacyRepos, UserRepos);
        OrderService = new OrderService(ProductRepos, UserRepos, OrderRepos, PrescriptionRepos);
    }

    protected static Client ValidClient() => new Client(0, "test", "testtest1*");
    protected readonly IRepos<RegisteredUser, UserDto> UserRepos = new UserRepos();
    private static IRepos<Medicine, MedicineDto> MedicineRepos => new LocalMedicineRepos(@".\medicinetest.json");

    private static IRepos<Prescription, PrescriptionDto> PrescriptionRepos =>
        new LocalPrescriptionRepos(MedicineRepos, @".\prescriptionstest.json");

    protected readonly IRepos<Product, ProductDto> ProductRepos = new ProductRepos(MedicineRepos);
    protected readonly IRepos<Pharmacy, PharmacyDto> PharmacyRepos;
    protected readonly IRepos<Order, OrderDto> OrderRepos;
    public IOrderService OrderService;
    public IUserService ValidUserService() => new ClientService(ValidClient(), ProductRepos, OrderRepos, PharmacyRepos, UserRepos,
        OrderService);

    public IClientService ValidServiceFromClient(Client user) => new ClientService(user, ProductRepos, OrderRepos,
        PharmacyRepos, UserRepos,
        OrderService);

}