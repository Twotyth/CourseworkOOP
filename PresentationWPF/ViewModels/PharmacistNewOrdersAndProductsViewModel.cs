using System;
using System.Collections.ObjectModel;
using System.Linq;
using Application.InfoObjects;
using Application.Services;
using Application.Services.UserServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DataLayer.Dtos;
using DataLayer.Repositories;
using Domain.Common;
using Domain.Entities;
using PresentationWPF.Messages;
using PresentationWPF.Stores;

namespace PresentationWPF.ViewModels;

public partial class PharmacistNewOrdersAndProductsViewModel : ObservableObject,
    IRecipient<StockOrderIdRequestMessage>, IRecipient<OrderAcceptedMessage>
{
    private readonly IMessenger _messenger;
    private INavigationService _backNavigationService;
    private readonly INavigationService _personalCabinetNavigationService;
    private readonly IPharmacistService _pharmacist;

    public bool OrdersEmpty { get; }
    public ObservableCollection<PharmacistOrderViewModel> OrdersToAccept { get; }
    public ObservableCollection<RegisterProductItemViewModel> ViewingProductsToRegister { get; }

    public PharmacistNewOrdersAndProductsViewModel(
        IMessenger messenger,
        AccountStore accountStore, IRepos<Product, ProductDto> productRepos,
        INavigationService personalCabinetNavigationService,
        INavigationService backNavigationService
    )
    {
        _messenger = messenger;
        _backNavigationService = backNavigationService;
        _personalCabinetNavigationService = personalCabinetNavigationService;
        _pharmacist = accountStore.CurrentUser as IPharmacistService ??
                      throw new ArgumentException("Current user was not expected. Expected user: Pharmacist");

        OrdersToAccept = new(((IRetrieving<OrderInfo>)_pharmacist).Get()
            .Where(i => i.Status == OrderStatus.Delivering)
            .Select(i => new PharmacistOrderViewModel(_messenger, i.Id, this)));

        OrdersEmpty = !OrdersToAccept.Any();

        ViewingProductsToRegister = new(productRepos.Where(i => i.IsOnSale)
            .Select(i => new RegisterProductItemViewModel(
                i.Id, this, i.FullName
            ))
        );
    }

    [RelayCommand]
    private void PersonalCabinetNavigate()
    {
        _personalCabinetNavigationService.Navigate();
    }

    [RelayCommand]
    private void BackNavigate()
    {
        _backNavigationService.Navigate();
    }
    
    
    
    public partial class PharmacistOrderViewModel : ObservableObject
    {
        private readonly IMessenger _messenger;
        public uint Id { get; }
        private readonly IPharmacistService _service;

        public PharmacistOrderViewModel(
            IMessenger messenger,
            uint id, PharmacistNewOrdersAndProductsViewModel ordersAndProductsViewModel
            )
        {
            _messenger = messenger;
            Id = id;
            _service = ordersAndProductsViewModel._pharmacist;
        }
        
        [RelayCommand]
        private void AcceptOrder()
        {
            _service.AcceptOrder(Id);
            _messenger.Send(new OrderAcceptedMessage(Id));
        }
    }

    public void Receive(StockOrderIdRequestMessage message)
    {
        if (message.HasReceivedResponse) return;
        
        message.Reply(((IRetrieving<OrderInfo>)_pharmacist).Get()
            .Where(i => i.Status == OrderStatus.Delivered)
            .Select(i => i.Id));
    }

    public partial class RegisterProductItemViewModel : ObservableObject
    {
        public uint Id { get; }
        
        public string DisplayName { get; }

        [ObservableProperty]
        private uint quantity = 1;
        
        private readonly IPharmacistService _service;

        public RegisterProductItemViewModel(uint id, PharmacistNewOrdersAndProductsViewModel pharmacistNewOrdersAndProductsViewModel, string displayName)
        {
            Id = id;
            DisplayName = displayName;
            _service = pharmacistNewOrdersAndProductsViewModel._pharmacist;
        }
        
        [RelayCommand]
        private void RegisterToPharmacy()
        {
            _service.RegisterNewProducts(new []{ (Id, Quantity)});
        }

        [RelayCommand]
        private void IncreaseQuantity()
        {
            Quantity++;
        }

        [RelayCommand]
        private void DecreaseQuantity()
        {
            if (Quantity == 1) return;
            Quantity--;
        }
    }

    public void Receive(OrderAcceptedMessage message)
    {
        var item = OrdersToAccept.FirstOrDefault(i => i.Id == message.OrderId, null);

        if (item == null) return;

        OrdersToAccept.Remove(item);
    }
}

