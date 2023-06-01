using System;
using System.Collections.ObjectModel;
using System.Linq;
using Application.Enums;
using Application.InfoObjects;
using Application.Services;
using Application.Services.UserServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PresentationWPF.Messages;
using PresentationWPF.Stores;

namespace PresentationWPF.ViewModels;

public partial class PharmacistCheckoutViewModel : ObservableObject,
    IRecipient<CheckItemAddedMessage>, IRecipient<CheckItemQuantityChanged>,
    IRecipient<CheckItemRemovedMessage>, IRecipient<StockOrderIdRequestMessage>,
    IRecipient<OrderAcceptedMessage>
{
    private readonly IMessenger _messenger;
    private readonly CheckStore _checkStore;
    private readonly IPaymentService _paymentService;
    private readonly INavigationService _personalCabinetNavigationService;
    private readonly INavigationService _newOrdersAndProductsNavigationService;
    private readonly IPharmacistService _pharmacistService;
    public ObservableCollection<StockItemViewModel> ViewingStockItems { get; }
    public ObservableCollection<CheckItemViewModel> ViewingCheck { get; } = new();

    public ObservableCollection<uint> OrdersIds { get; }

    [ObservableProperty]
    private uint? selectedOrder;

    [ObservableProperty]
    private string prescriptionId = "";

    [ObservableProperty]
    private string clientId = "";

    [ObservableProperty]
    private string balanceMoneyToWithdraw = "";
    
    
    [ObservableProperty]
    private string errorMessage = "";

    [ObservableProperty]
    private string titleSearched = "";

    
    [ObservableProperty]
    private decimal totalPrice = 0;

    

    public PharmacistCheckoutViewModel(
        IMessenger messenger,
        AccountStore accountStore, CheckStore checkStore, 
        IPaymentService paymentService,
        INavigationService personalCabinetNavigationService,
        INavigationService newOrdersAndProductsNavigationService
    )
    {
        _messenger = messenger;
        _checkStore = checkStore;
        _paymentService = paymentService;
        _personalCabinetNavigationService = personalCabinetNavigationService;
        _newOrdersAndProductsNavigationService = newOrdersAndProductsNavigationService;
        _pharmacistService = accountStore.CurrentUser as IPharmacistService ??
                             throw new ArgumentException("Current user was not expected. Expected user: Pharmacist");

        messenger.RegisterAll(this);
        
        ViewingStockItems = new ObservableCollection<StockItemViewModel>(
            ((IRetrieving<(ProductInfo, uint)>)_pharmacistService)
            .Get()
            .Select(i => new StockItemViewModel(i.Item1, i.Item2, checkStore))
        );
        
        
        
        var response = _messenger.Send<CheckItemsRequestMessage>();
        Receive(response);

        var responseOrders = ((IRetrieving<OrderInfo>)_pharmacistService).Get().Select(i => i.Id);
        OrdersIds = new(responseOrders);
    }

    private void AddCheckItem(CheckItem item)
    {
        ViewingCheck.Add(new CheckItemViewModel(_messenger, item.Id, item.Quantity, ViewingStockItems.First(i => i.ProductInfo.Id == item.Id).Quantity, item.Name, item.Manufacturer, item.Price));
        TotalPrice = ViewingCheck.Sum(i => i.Price * i.Quantity);
    }
    
    [RelayCommand]
    private void Search()
    {
        var filteredItems = ((IRetrieving<(ProductInfo, uint)>)_pharmacistService).Get()
            .Where(i => !TitleSearched.Any() || i.Item1.FullName.Contains(TitleSearched) || i.Item1.Description.Contains(TitleSearched))
            .Select(x => new StockItemViewModel(x.Item1, x.Item2, _checkStore)
        );
        
        ViewingStockItems.Clear();
        
        foreach (var item in filteredItems)
        {
            ViewingStockItems.Add(item);
        }
    }

    [RelayCommand]
    private void DeselectOrder()
    {
        SelectedOrder = null;
    }

    [RelayCommand]
    private void Checkout()
    {
        uint? prescription = null;
        uint? client = null;
        decimal? withdrawMoney = null;

        if (PrescriptionId.Any())
        {
            if (!uint.TryParse(PrescriptionId, out var prescription2))
            {
                ErrorMessage = "Provided prescription ID is not valid";
                return;
            }

            prescription = prescription2;
        }

        if (ClientId.Any())
        {
            if (!uint.TryParse(ClientId, out var clientId2))
            {
                ErrorMessage = "Provided client ID is not valid";
                return;
            }

            client = clientId2;
        }
        
        if (SelectedOrder != null)
        {
            var orderResult = _pharmacistService.GiveOutOrder(SelectedOrder.Value, prescription);
            ErrorMessage = orderResult switch
            {
                OrderGiveOutResult.Success => "",
                OrderGiveOutResult.PrescriptionRequired => "Prescription required to give out order",
                OrderGiveOutResult.OrderNotFound => "Order was not found",
                _ => throw new ArgumentOutOfRangeException()
            };

            if (orderResult != OrderGiveOutResult.Success) return;

            _messenger.Send<OrderGaveOutNotification>();
        }

        if (BalanceMoneyToWithdraw.Any())
        {
            if (!decimal.TryParse(BalanceMoneyToWithdraw, out var balanceMoneyToWithdraw2))
            {
                ErrorMessage = "Provided withdraw money is not valid";
                return;
            }

            withdrawMoney = balanceMoneyToWithdraw2;
        }

        var response = _messenger.Send<CheckItemsRequestMessage>();
        Receive(response);
        
        var productsToSell = response.Response.ToDictionary(i => i.Id, i => i.Quantity);

        
        SellResult result;
        try
        {
            result = _pharmacistService.Sell(productsToSell, _paymentService, prescription, client, withdrawMoney);
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
            return;
        }

        ErrorMessage = result switch
        {
            SellResult.Ok => "",
            SellResult.PrescriptionRequired => "Prescription is required to sell",
            SellResult.PaymentFailed => "Payment failed. Please try again",
            _ => throw new ArgumentOutOfRangeException()
        };
        
        if (result != SellResult.Ok) return;
        ViewingCheck.Clear();

        foreach (var product in productsToSell)
        {
            var item = ViewingStockItems.FirstOrDefault(i => i.ProductInfo.Id == product.Key, null);
            if (item == null) throw new Exception("woohoo");
            item.Quantity -= product.Value;
        }
        
        _messenger.Send<SellProductsMessage>();
    }

    [RelayCommand]
    private void OrdersAndProductsNavigate()
    {
        _newOrdersAndProductsNavigationService.Navigate();
    }

    [RelayCommand]
    private void PersonalCabinetNavigate()
    {
        _personalCabinetNavigationService.Navigate();
    }

    private void Receive(CheckItemsRequestMessage message)
    {
        if (!message.HasReceivedResponse) return;

        ViewingCheck.Clear();
        
        foreach (var item in message.Response)
        {
            AddCheckItem(item);
        }
    }

    public void Receive(CheckItemAddedMessage message)
    {
        var item = ViewingCheck.FirstOrDefault(i => i.Id == message.item.Id, null);

        if (item == null) AddCheckItem(message.item);
        else item.Quantity++;
    }

    public void Receive(CheckItemQuantityChanged message)
    {
        var item = ViewingCheck.FirstOrDefault(i => i!.Id == message.Id, null);

        if (item == null) return;
        item.Quantity = message.Quantity;
        TotalPrice = ViewingCheck.Sum(i => i.Price*i.Quantity);
    }

    public void Receive(CheckItemRemovedMessage message)
    {
        var item = ViewingCheck.FirstOrDefault(i => i!.Id == message.Id, null);

        if (item == null) return;
        ViewingCheck.Remove(item);
        TotalPrice = ViewingCheck.Sum(i => i.Price*i.Quantity);
    }

    public void Receive(StockOrderIdRequestMessage message)
    {
        if (!message.HasReceivedResponse) return;
        
        OrdersIds.Clear();
        foreach (var id in message.Response)
        {
            OrdersIds.Add(id);
        }
    }

    public void Receive(OrderAcceptedMessage message)
    {
        OrdersIds.Add(message.OrderId);
    }
}

public sealed partial class StockItemViewModel : ObservableObject
{
    private readonly CheckStore _store;
    public ProductInfo ProductInfo { get; init; }


    [ObservableProperty]
    public uint quantity;

    public StockItemViewModel(ProductInfo productInfo, uint quantity, CheckStore store)
    {
        _store = store;
        ProductInfo = productInfo;
        Quantity = quantity;
    }

    [RelayCommand]
    private void AddToCheck()
    {
        _store.AddToCheck(new CheckItem(ProductInfo.Id, 1, ProductInfo.FullName, ProductInfo.Manufacturer, ProductInfo.Price));
    }
}

public sealed partial class CheckItemViewModel : ObservableObject
{
    private readonly IMessenger _messenger;
    public uint Id { get; }
    public uint TotalQuantity { get; }

    [ObservableProperty]
    public uint quantity;
    public string Name { get; }
    public string Manufacturer { get; }

    public decimal Price { get; }

    [ObservableProperty]
    private decimal totalPrice;
    
    public CheckItemViewModel(IMessenger messenger, uint id, uint quantity, uint totalQuantity, string name, string manufacturer, decimal price)
    {
        _messenger = messenger;
        Id = id;
        TotalQuantity = totalQuantity;
        Quantity = quantity;
        Name = name;
        Manufacturer = manufacturer;
        Price = price;
        TotalPrice = Price;
    }


    [RelayCommand]
    public void RemoveFromCheck()
    {
        _messenger.Send(new CheckItemRemovedMessage(Id));
    }

    [RelayCommand]
    public void IncreaseQuantity()
    {
        if (TotalQuantity == Quantity) return;
        Quantity++;
        TotalPrice = Price * Quantity;
        _messenger.Send(new CheckItemQuantityChanged(Id, Quantity));
    }

    [RelayCommand]
    public void DecreaseQuantity()
    {
        if (Quantity == 1) return;
        Quantity--;
        TotalPrice = Price * Quantity;
        _messenger.Send(new CheckItemQuantityChanged(Id, Quantity));
    }
}