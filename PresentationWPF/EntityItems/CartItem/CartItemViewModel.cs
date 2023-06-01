using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PresentationWPF.Messages;
using PresentationWPF.Stores;

namespace PresentationWPF.EntityItems.CartItem;

public partial class CartItemViewModel : ObservableObject
{
    private readonly IMessenger _messenger;
    public uint Id { get; }
    
    public string Name { get; }
    public string Manufacturer { get; }
    public bool IsPrescriptionRequired { get; }
    public decimal Price { get; }

    [ObservableProperty]
    private uint quantity;

    public CartItemViewModel(IMessenger messenger, uint id, uint quantity, string name, string manufacturer, bool isPrescriptionRequired, decimal price)
    {
        _messenger = messenger;
        Id = id;
        Quantity = quantity;
        Name = name;
        Manufacturer = manufacturer;
        IsPrescriptionRequired = isPrescriptionRequired;
        Price = price;
    }

    [RelayCommand]
    public void RemoveFromCart()
    {
        _messenger.Send(new CartItemRemovedMessage(Id));
    }

    [RelayCommand]
    public void IncreaseQuantity()
    {
        Quantity++;
        _messenger.Send(new CartItemQuantityChanged(Id, Quantity));
    }

    [RelayCommand]
    public void DecreaseQuantity()
    {
        if (Quantity == 1) return;
        Quantity--;
        _messenger.Send(new CartItemQuantityChanged(Id, Quantity));
    }
}