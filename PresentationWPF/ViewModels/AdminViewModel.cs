using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Application.Enums;
using Application.InfoObjects;
using Application.Services;
using Application.Services.UserServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DataLayer.Dtos;
using Domain.Common;
using Domain.Entities;
using PresentationWPF.Messages;
using PresentationWPF.Stores;

// ReSharper disable InconsistentNaming

namespace PresentationWPF.ViewModels;

public sealed partial class AdminViewModel : ObservableObject,
    IRecipient<ProductsChangedNotification>, IRecipient<PharmaciesChangedNotification>,
    IRecipient<PharmacistsChangedNotification>, IRecipient<OrdersChangedNotification>
{
    private readonly IAdminService _adminService;
    private readonly IMessenger _messenger;
    private readonly MainViewModel _mvm;
    private readonly INavigationService _personalCabinetNavigationService;
    private readonly INavigationService _newEntityNavigationService;
    private readonly INavigationService _newPharmacyNavigationService;
    private readonly INavigationService _newPharmacistNavigationService;

    [ObservableProperty] private string errorMessage = "";


    public IEnumerable<ProductViewModel> ProductViewModels =>
        ((IRetrieving<ProductInfo>)_adminService).Get().Select(
            i => new ProductViewModel(
                _messenger, i.Id, i.Name, i.Price, i.Manufacturer, i.Description, i.IsOnSale,
                i.MedicineInfo == null
                    ? null
                    : new ProductMedicineInfo(
                        i.MedicineInfo.MedicineId, i.MedicineInfo.Dosage, i.MedicineInfo.Quantity,
                        i.MedicineInfo.DosageForm, i.MedicineInfo.QuantityForm, i.MedicineInfo.ConsumptionForm
                    ), this
            )
        );

    public IEnumerable<PharmacyViewModel> PharmacyViewModels =>
        ((IRetrieving<PharmacyInfo>)_adminService).Get().Select(
            i => new PharmacyViewModel(_messenger, this, i.Id, i.Address, i.WorkingPharmacistId)
        );

    public IEnumerable<PharmacistViewModel> PharmacistViewModels =>
        ((IRetrieving<PharmacistInfo>)_adminService).Get().Select(
            i => new PharmacistViewModel(_messenger, this, i.Id, i.Salary)
        );

    public IEnumerable<OrderViewModel> OrderViewModels =>
        ((IRetrieving<OrderInfo>)_adminService).Get().Select(
            i => new OrderViewModel(_messenger, this, i.Id, i.ClientId, i.Status, i.DeliveryPharmacyId)
        );

    public IEnumerable<ClientViewModel> ClientViewModels =>
        ((IRetrieving<ClientInfo>)_adminService).Get().Select(
            i => new ClientViewModel(i.Id, i.Balance)
        );

    public AdminViewModel(
        IMessenger messenger, MainViewModel mvm, AccountStore accountStore,
        INavigationService personalCabinetNavigationService,
        INavigationService newEntityNavigationService
    )
    {
        if (accountStore.CurrentUser is not IAdminService adminService)
        {
            throw new ArgumentException("Current user was not expected. Expected user: Admin");
        }
        
        messenger.RegisterAll(this);

        _messenger = messenger;
        _mvm = mvm;
        _personalCabinetNavigationService = personalCabinetNavigationService;
        _newEntityNavigationService = newEntityNavigationService;

        _adminService = adminService;
    }

    [RelayCommand]
    private void PersonalCabinetNavigate()
    {
        _personalCabinetNavigationService.Navigate();
    }

    [RelayCommand]
    private void NewEntityNavigation()
    {
        _newEntityNavigationService.Navigate();
    }

    public void Receive(ProductsChangedNotification message)
    {
        OnPropertyChanged(nameof(ProductViewModels));
    }

    public void Receive(PharmaciesChangedNotification message)
    {
        OnPropertyChanged(nameof(PharmacyViewModels));
    }

    public void Receive(PharmacistsChangedNotification message)
    {
        OnPropertyChanged(nameof(PharmacistViewModels));
    }

    public void Receive(OrdersChangedNotification message)
    {
        OnPropertyChanged(nameof(OrderViewModels));
    }

    public sealed partial class ClientViewModel : ObservableObject
    {
        public uint Id { get; }
        public decimal Balance { get; }

        public ClientViewModel(uint id, decimal login)
        {
            Id = id;
            Balance = login;
        }
    }

    public sealed partial class ProductViewModel : ObservableObject
    {
        private readonly IMessenger _messenger;
        private readonly AdminViewModel _awm;
        public uint Id { get; }
        public string Name { get; set; }

        public string FullName => $"{Name}, {MedicineInfo?.Dosage} " +
                                  $"{(MedicineInfo?.DosageForm == null ? null : DosageQuantityConverter(MedicineInfo.DosageForm))} " +
                                  $"{MedicineInfo?.Quantity} {(MedicineInfo?.QuantityForm == null ? null : QuantityConverter(MedicineInfo.QuantityForm))}";

        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        
        public bool IsOnSale { get; set; }
        public ProductMedicineInfo? MedicineInfo { get; set; }
        

        public ProductViewModel(
            IMessenger messenger,
            uint id, string name, decimal price, string manufacturer,
            string description, bool isOnSale,
            ProductMedicineInfo? medicineInfo, AdminViewModel awm
        )
        {
            _messenger = messenger;
            _awm = awm;
            Id = id;
            Name = name;
            Price = price;
            Manufacturer = manufacturer;
            Description = description;
            IsOnSale = isOnSale;
            MedicineInfo = medicineInfo;
        }
        
        [RelayCommand]
        private void ShowEdit()
        {
            _awm._newEntityNavigationService.Navigate();
            _messenger.Send(new EditProductMessage(new ProductInfo(
                Id, Name, Manufacturer, Description, IsOnSale, MedicineInfo, Price
                )
            ));
        }


        [RelayCommand]
        public void Delete()
        {
            var result = _awm._adminService.DeleteProduct(Id);

            _awm.ErrorMessage = result switch
            {
                DeleteProductResult.NotDeliveringRequired =>
                    "Product is delivering, try to withdraw it from sale first",
                DeleteProductResult.ProductNotFound => "Product was not found",
                DeleteProductResult.Success => "",
                _ => throw new ArgumentOutOfRangeException()
            };

            if (result != DeleteProductResult.Success) return;

            _messenger.Send<ProductsChangedNotification>();
        }

        [RelayCommand]
        public void ChangeWithdraw()
        {
            if (IsOnSale)
            {
                var result = _awm._adminService.WithdrawFromSale(Id);

                _awm.ErrorMessage = result switch
                {
                    WithdrawFromSaleResult.ProductNotFound => "Product was not found",
                    WithdrawFromSaleResult.ProductAlreadyWithdrawn => "Product is already withdrawn from sale",
                    WithdrawFromSaleResult.Success => "",
                    _ => throw new ArgumentOutOfRangeException()
                };

                if (result != WithdrawFromSaleResult.Success) return;
            }
            else
            {
                var result = _awm._adminService.ReturnToSale(Id);

                _awm.ErrorMessage = result switch
                {
                    ReturnToSaleResult.Success => "",
                    ReturnToSaleResult.ProductNotFound => "Product was not found",
                    ReturnToSaleResult.ProductNotWithdrawn => "Product is not withdrawn from sale",
                    _ => throw new ArgumentOutOfRangeException()
                };

                if (result != ReturnToSaleResult.Success) return;
            }

            _messenger.Send<ProductsChangedNotification>();
        }


        private static string QuantityConverter(QuantityForm medicineInfoQuantityForm) => medicineInfoQuantityForm switch
        {
            QuantityForm.Pieces => "pcs",
            QuantityForm.Milliliters => "ml",
            QuantityForm.Grams => "g",
            _ => throw new ArgumentOutOfRangeException(nameof(medicineInfoQuantityForm), medicineInfoQuantityForm, null)
        };

        private static string DosageQuantityConverter(DosageForm dosageForm) => dosageForm switch
        {
            DosageForm.Mg => "mg",
            DosageForm.Ml => "ml",
            DosageForm.G => "g",
            _ => throw new ArgumentOutOfRangeException(nameof(dosageForm), dosageForm, null)
        };
    }

    public partial class PharmacyViewModel : ObservableObject
    {
        private readonly IMessenger _messenger;
        private readonly AdminViewModel _awm;

        public uint Id { get; }
        public string Address { get; set; }
        public uint? WorkingPharmacistId { get; set; }

        public PharmacyViewModel(
            IMessenger messenger, AdminViewModel awm,
            uint id, string address, uint? workingPharmacistId
        )
        {
            _messenger = messenger;
            _awm = awm;

            Id = id;
            Address = address;
            WorkingPharmacistId = workingPharmacistId;
        }

        [RelayCommand]
        private void ShowEdit()
        {
            _awm._newEntityNavigationService.Navigate();
            _messenger.Send(new EditPharmacyMessage(new PharmacyInfo(Id, Address, WorkingPharmacistId)));
        }

        [RelayCommand]
        private void Delete()
        {
            var result = _awm._adminService.DeletePharmacy(Id);

            _awm.ErrorMessage = result switch
            {
                DeletePharmacyResult.PharmacyNotFound => "Pharmacy was not found",
                DeletePharmacyResult.Success => "",
                _ => throw new ArgumentOutOfRangeException()
            };

            if (result != DeletePharmacyResult.Success) return;

            _messenger.Send<PharmaciesChangedNotification>();
        }
    }

    public partial class PharmacistViewModel : ObservableObject
    {
        private readonly IMessenger _messenger;
        private readonly AdminViewModel _awm;

        public uint Id { get; }
        public uint Salary { get; set; }


        
        public PharmacistViewModel(
            IMessenger messenger, AdminViewModel awm, uint id, uint salary
        )
        {
            Id = id;
            Salary = salary;
            _messenger = messenger;
            _awm = awm;
        }

        [RelayCommand]
        private void ShowEdit()
        {
            _awm._newEntityNavigationService.Navigate();
            _messenger.Send(new EditPharmacistMessage(new PharmacistInfo(Id, "", Salary)));
        }
        

        [RelayCommand]
        private void Delete()
        {
            var result = _awm._adminService.DeleteUser(Id);

            _awm.ErrorMessage = result switch
            {
                DeleteUserResult.UserNotFound => "Pharmacist was not found",
                DeleteUserResult.Success => "",
                _ => throw new ArgumentOutOfRangeException()
            };

            if (result != DeleteUserResult.Success) return;

            _messenger.Send<PharmacistsChangedNotification>();
        }
    }

    public partial class OrderViewModel : ObservableObject
    {
        private readonly IMessenger _messenger;
        private readonly AdminViewModel _adminService;

        public OrderViewModel(IMessenger messenger, AdminViewModel adminService, uint id, uint? clientId,
            OrderStatus status, uint deliveryPharmacyId)
        {
            _messenger = messenger;
            _adminService = adminService;
            Id = id;
            ClientId = clientId;
            Status = status;
            DeliveryPharmacyId = deliveryPharmacyId;
        }

        public uint Id { get; }

        public uint? ClientId { get; }

        public OrderStatus Status { get; }

        public uint DeliveryPharmacyId { get; }

        [RelayCommand]
        private void CancelOrder()
        {
            var result = _adminService._adminService.CancelOrder(Id);

            _adminService.ErrorMessage = result switch
            {
                CancelOrderResult.DeliveringOrderNotFound => "Order that is delivering was not found",
                CancelOrderResult.Success => "",
                _ => throw new ArgumentOutOfRangeException()
            };

            if (result != CancelOrderResult.Success) return;


            _messenger.Send<OrdersChangedNotification>();
        }
    }
}