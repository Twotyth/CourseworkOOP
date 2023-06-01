using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Media;
using Application.InfoObjects;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PresentationWPF.EntityItems.CartItem;
using PresentationWPF.Messages;
using PresentationWPF.Stores;

// ReSharper disable InconsistentNaming

namespace PresentationWPF.ViewModels;

internal class IsNotNullAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is not null;
    }
}

public abstract partial class CartViewModelBase : ObservableValidator, 
    IRecipient<CartItemAddedMessage>, IRecipient<CartItemRemovedMessage>,
    IRecipient<CartItemQuantityChanged>
{
    private readonly IMessenger _messenger;
    private readonly INavigationService _backNavigationService;
    private readonly INavigationService _profileNavigationService;
    public ObservableCollection<CartItemViewModel> CartItems { get; } = new();

    [ObservableProperty]
    private PharmacyInfo? selectedDeliveryPharmacy;

    [ObservableProperty]
    private string prescriptionId = "";

    [ObservableProperty]
    private decimal totalPrice = 0;

    [ObservableProperty]
    private string message = "";

    [ObservableProperty]
    private SolidColorBrush messageBrush = new(Colors.Red);
    
    public PharmacyInfo[] PharmacyAddresses { get; set; } = null!;

    protected CartViewModelBase(
        IMessenger messenger, INavigationService backNavigationService,
        INavigationService profileNavigationService
        )
    {
        _messenger = messenger;
        _backNavigationService = backNavigationService;
        _profileNavigationService = profileNavigationService;
        messenger.RegisterAll(this);
        var responseCarItems = messenger.Send<CartItemsRequestMessage>();
        Receive(responseCarItems);
    }
    private void AddCartItem(CartItem item)
    {
        CartItems.Add(new CartItemViewModel(_messenger, item.Id, item.Quantity, item.Name, item.Manufacturer,
            item.IsPrescriptionRequired, item.Price));

        TotalPrice = CartItems.Sum(i => i.Price);
    }

    [RelayCommand]
    protected abstract void Checkout();

    [RelayCommand]
    private void Back()
    {
        _backNavigationService.Navigate();
    }

    [RelayCommand]
    private void ProfileNavigate()
    {
        _profileNavigationService.Navigate();
    }
    
    protected virtual void Receive(CartItemsRequestMessage message)
    {
        if (!message.HasReceivedResponse)
        {
            return;
        }
        
        CartItems.Clear();
        
        foreach (var cartItem in message.Response)
        {
            AddCartItem(cartItem);
        }
    }

    public virtual void Receive(CartItemAddedMessage message)
    {
        CartItems.Add(new(
            _messenger,
            message.Item.Id,
            message.Item.Quantity,
            message.Item.Name,
            message.Item.Manufacturer,
            message.Item.IsPrescriptionRequired,
            message.Item.Price)
        );

        TotalPrice += message.Item.Price;
    }

    public virtual void Receive(CartItemRemovedMessage message)
    {
        var item = CartItems.FirstOrDefault(i => i.Id == message.ItemId, null);

        if (item == null) return;

        CartItems.Remove(item);
        TotalPrice -= item.Price*item.Quantity;
    }

    public virtual void Receive(CartItemQuantityChanged message)
    {
        TotalPrice = CartItems.Sum(i => i.Price*i.Quantity);
    }
}

