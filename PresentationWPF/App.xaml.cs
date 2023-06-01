using System;
using System.IO;
using System.Windows;
using Application.Services;
using Application.Services.UserServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using DataLayer.Dtos;
using DataLayer.Repositories;
using Domain.Entities;
using Domain.Entities.Users;
using Infrastructure.Services;
using Infrastructure.Services.UserServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PresentationWPF.Stores;
using PresentationWPF.ViewModels;
using PresentationWPF.Views;

namespace PresentationWPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    internal static IConfiguration Config { get; private set; } = null!;
    public static IServiceProvider Services { get; private set; } = null!;
    
    public App()
    {
        
        Config = new ConfigurationBuilder().AddJsonFile(
            Path.Combine(
                Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName, "appsettings.json"
            )
        ).Build();

        var s = new ServiceCollection();

        ConfigureRepositories(s);
        ConfigureUserServices(s);
        ConfigureViews(s);
        
        Services = s.BuildServiceProvider();


        Services.GetRequiredService<MainViewModel>().CurrentViewModel =
            Services.GetRequiredService<GuestCatalogViewModel>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var main = Services.GetRequiredService<MainView>();
        main.DataContext = Services.GetRequiredService<MainViewModel>();
        main.Show();
        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        var path = Config["SaveDirectory"]!;
        var userRepos = Services.GetRequiredService<IRepos<RegisteredUser, UserDto>>();
        var productRepos = Services.GetRequiredService<IRepos<Product, ProductDto>>();
        var pharmacyRepos = Services.GetRequiredService<IRepos<Pharmacy, PharmacyDto>>();
        var orderRepos = Services.GetRequiredService<IRepos<Order, OrderDto>>();
        try
        {
            var ur = userRepos.Serialize();
            var prr = productRepos.Serialize();
            var phr = pharmacyRepos.Serialize();
            var or = orderRepos.Serialize();

            File.WriteAllText(Path.Combine(path, "user_repos.json"), ur);
            File.WriteAllText(Path.Combine(path, "product_repos.json"), prr);
            File.WriteAllText(Path.Combine(path, "pharmacy_repos.json"), phr);
            File.WriteAllText(Path.Combine(path, "order_repos.json"), or);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        base.OnExit(e);
    }

    private void ConfigureRepositories(ServiceCollection s)
    {
        var path = Config["SaveDirectory"]!;
        var medicineProvider = Config["MedicineProvider"]!;
        var prescriptionProvider = Config["PrescriptionProvider"]!;
        IRepos<Medicine, MedicineDto> medicineRepos = new LocalMedicineRepos(medicineProvider);
        IRepos<Prescription, PrescriptionDto> prescriptionRepos =
            new LocalPrescriptionRepos(medicineRepos, prescriptionProvider);
        
        
        IRepos<RegisteredUser, UserDto> userRepos = new UserRepos();
        IRepos<Product, ProductDto> productRepos = new ProductRepos(medicineRepos);
        IRepos<Pharmacy, PharmacyDto> pharmacyRepos = new PharmacyRepos(userRepos, productRepos);
        IRepos<Order, OrderDto> orderRepos = new OrderRepos(productRepos, pharmacyRepos, userRepos);
        try
        {
            userRepos.Deserialize(File.ReadAllText(Path.Combine(path, "user_repos.json")));
            productRepos.Deserialize(File.ReadAllText(Path.Combine(path, "product_repos.json")));
            pharmacyRepos.Deserialize(File.ReadAllText(Path.Combine(path, "pharmacy_repos.json")));
            orderRepos.Deserialize(File.ReadAllText(Path.Combine(path, "order_repos.json")));
        }
        catch
        {
            userRepos = new UserRepos();
            productRepos = new ProductRepos(medicineRepos);
            pharmacyRepos = new PharmacyRepos(userRepos, productRepos);
            orderRepos = new OrderRepos(productRepos, pharmacyRepos, userRepos);
        }
        s.AddSingleton(medicineRepos);
        s.AddSingleton(prescriptionRepos);
        s.AddSingleton(userRepos);
        s.AddSingleton(productRepos);
        s.AddSingleton(pharmacyRepos);
        s.AddSingleton(orderRepos);
        
        s.AddTransient<IOrderService, OrderService>();
    }

    private static void ConfigureUserServices(ServiceCollection s)
    {
        s.AddTransient<IGuestService, GuestService>();
        s.AddTransient<IPaymentService, CashPaymentService>();
    }

    private static void ConfigureViews(ServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IMessenger>(StrongReferenceMessenger.Default);
        
        
        serviceCollection.AddSingleton<CartStore>();
        serviceCollection.AddSingleton<FilterStore>();
        serviceCollection.AddSingleton<AccountStore>();
        serviceCollection.AddSingleton<CheckStore>();

        serviceCollection.AddSingleton<MainView>();
        serviceCollection.AddSingleton<MainViewModel>();

        
        

        serviceCollection.AddTransient<LoginViewModel>(
            s =>
            {
                ObservableObject LoginNavigationGetter()
                {
                    var account = s.GetRequiredService<AccountStore>().CurrentUser;

                    return account switch
                    {
                        null => s.GetRequiredService<GuestCatalogViewModel>(),
                        IClientService => s.GetRequiredService<ClientCatalogViewModel>(), //s.GetRequiredService<ClientCartViewModel>(),
                        IPharmacistService => s.GetRequiredService<PharmacistCheckoutViewModel>(), //s.GetRequiredService<PharmacistPageViewModel>(),
                        IAdminService => s.GetRequiredService<AdminViewModel>(),
                        _ => throw new ArgumentOutOfRangeException(nameof(account))
                    };
                }
                
                var a = new LoginViewModel(
                    s.GetRequiredService<MainViewModel>(),
                    s.GetRequiredService<AccountStore>(),
                    s.GetRequiredService<IGuestService>(),
                    ModalNavigationService.Get<SignUpViewModel>(s),
                    new NavigationService(
                        s.GetRequiredService<MainViewModel>(),
                        LoginNavigationGetter
                    )
                );

                return a;
            });
        serviceCollection.AddTransient<LoginView>(
            s => new LoginView()
            {
                DataContext = s.GetRequiredService<LoginViewModel>()
            }
        );

        serviceCollection.AddTransient<SignUpViewModel>(
            s => new SignUpViewModel(
                s.GetRequiredService<MainViewModel>(),
                s.GetRequiredService<AccountStore>(),
                s.GetRequiredService<IGuestService>(),
                NavigationService.Get<ClientCatalogViewModel>(s)
            )
        );
        serviceCollection.AddTransient<SignUpView>(
            s => new SignUpView()
            {
                DataContext = s.GetRequiredService<SignUpViewModel>()
            }    
        );

        serviceCollection.AddTransient<FilterViewModel>();
        serviceCollection.AddTransient<FilterView>(s => new FilterView()
        {
            DataContext = s.GetRequiredService<FilterViewModel>()
        });
        
        serviceCollection.AddTransient<GuestCatalogViewModel>(
            s => new GuestCatalogViewModel(
                s.GetRequiredService<CartStore>(), s.GetRequiredService<FilterStore>(),
                s.GetRequiredService<IRepos<Product, ProductDto>>(), 
                ModalNavigationService.Get<LoginViewModel>(s),
                NavigationService.Get<GuestCartViewModel>(s),
                PopupNavigationService.Get<FilterViewModel>(s)

            )
        );
        serviceCollection.AddTransient<GuestCatalogView>(s => new GuestCatalogView()
        { 
            DataContext = s.GetRequiredService<GuestCatalogViewModel>()
        });
        
        serviceCollection.AddTransient<GuestCartViewModel>(s =>
        {
            return new GuestCartViewModel(
                s.GetRequiredService<IMessenger>(),
                s.GetRequiredService<IGuestService>(),
                NavigationService.Get<GuestCatalogViewModel>(s),
                ModalNavigationService.Get<LoginViewModel>(s),
                ModalNavigationService.Get<SignUpViewModel>(s)
            );
        });
        serviceCollection.AddTransient<GuestCartView>(s => new GuestCartView()
        {
            DataContext = s.GetRequiredService<GuestCartViewModel>()
        });

        serviceCollection.AddTransient<ClientCartViewModel>(s =>
        {
            return new ClientCartViewModel(
                s.GetRequiredService<IMessenger>(),
                s.GetRequiredService<AccountStore>(),
                NavigationService.Get<ClientCatalogViewModel>(s),
                ModalNavigationService.Get<ClientCabinetViewModel>(s)
            );
        });
        // serviceCollection.AddTransient<ClientCartView>(s => new ClientCartView()
        // {
        //     DataContext = s.GetRequiredService<ClientCartViewModel>()
        // });


        serviceCollection.AddTransient<ClientCatalogViewModel>(
            s => new ClientCatalogViewModel(
                s.GetRequiredService<AccountStore>(),
                s.GetRequiredService<CartStore>(), s.GetRequiredService<FilterStore>(), 
                s.GetRequiredService<IRepos<Product, ProductDto>>(),
                ModalNavigationService.Get<ClientCabinetViewModel>(s),
                NavigationService.Get<ClientCartViewModel>(s), 
                PopupNavigationService.Get<FilterViewModel>(s)
                
            )
        );
        serviceCollection.AddTransient<ClientCatalogView>(s => new ClientCatalogView()
        {
            DataContext = s.GetRequiredService<ClientCatalogViewModel>()
        });

        
        
        serviceCollection.AddTransient<PersonalCabinetViewModel>(
            s => new PersonalCabinetViewModel(
                s.GetRequiredService<IMessenger>(),
                s.GetRequiredService<AccountStore>(),
                s.GetRequiredService<MainViewModel>(),
                NavigationService.Get<GuestCatalogViewModel>(s)
            )
        );
        serviceCollection.AddTransient<PersonalCabinetView>(s => new PersonalCabinetView()
        {
            DataContext = s.GetRequiredService<PersonalCabinetViewModel>()
        });
        
        serviceCollection.AddTransient<ClientCabinetViewModel>(
            s => new ClientCabinetViewModel(
                s.GetRequiredService<IMessenger>(),
                s.GetRequiredService<AccountStore>(),
                s.GetRequiredService<MainViewModel>(),
                NavigationService.Get<GuestCatalogViewModel>(s)
            )    
        );
        serviceCollection.AddTransient<ClientCabinetView>(s => new ClientCabinetView()
        {
            DataContext = s.GetRequiredService<ClientCabinetViewModel>()
        });


        serviceCollection.AddTransient<AdminProductViewModel>();
        serviceCollection.AddTransient<AdminProductViewModel>();
        serviceCollection.AddTransient<AdminPharmacyViewModel>();
        serviceCollection.AddTransient<AdminPharmacistViewModel>();

        serviceCollection.AddTransient<AdminNewEntityViewModel>(s => new AdminNewEntityViewModel(
            s.GetRequiredService<IMessenger>(),
            s.GetRequiredService<MainViewModel>(), 
            s.GetRequiredService<AdminProductViewModel>,
            s.GetRequiredService<AdminPharmacistViewModel>,
            s.GetRequiredService<AdminPharmacyViewModel>
        ));

        serviceCollection.AddTransient<AdminViewModel>(s => new AdminViewModel
        (
            s.GetRequiredService<IMessenger>(),
            s.GetRequiredService<MainViewModel>(),
            s.GetRequiredService<AccountStore>(),
            ModalNavigationService.Get<PersonalCabinetViewModel>(s), 
            PopupNavigationService.Get<AdminNewEntityViewModel>(s)
        ));
        serviceCollection.AddTransient<AdminView>(s => new AdminView()
        {
            DataContext = s.GetRequiredService<AdminViewModel>()
        });



        serviceCollection.AddTransient<PharmacistCheckoutViewModel>(s => new PharmacistCheckoutViewModel(
            s.GetRequiredService<IMessenger>(), s.GetRequiredService<AccountStore>(),
            s.GetRequiredService<CheckStore>(), s.GetRequiredService<IPaymentService>(),
            ModalNavigationService.Get<PersonalCabinetViewModel>(s), 
            NavigationService.Get<PharmacistNewOrdersAndProductsViewModel>(s)
        ));
        serviceCollection.AddTransient<PharmacistCheckoutView>(s => new PharmacistCheckoutView()
        {
            DataContext = s.GetRequiredService<PharmacistCheckoutViewModel>()
        });

        serviceCollection.AddTransient<PharmacistNewOrdersAndProductsViewModel>(s =>
            new PharmacistNewOrdersAndProductsViewModel(
                s.GetRequiredService<IMessenger>(),
                s.GetRequiredService<AccountStore>(),
                s.GetRequiredService<IRepos<Product, ProductDto>>(),
                ModalNavigationService.Get<PersonalCabinetViewModel>(s),
                NavigationService.Get<PharmacistCheckoutViewModel>(s)
            ));
        serviceCollection.AddTransient<PharmacistNewOrdersAndProductsView>(s => new PharmacistNewOrdersAndProductsView()
        {
            DataContext = s.GetRequiredService<PharmacistNewOrdersAndProductsViewModel>()
        });
    }
}