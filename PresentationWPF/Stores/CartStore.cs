using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.Messaging;
using PresentationWPF.EntityItems.CartItem;
using PresentationWPF.Messages;

namespace PresentationWPF.Stores;

public class CartStore : 
    IRecipient<CartItemsRequestMessage>, IRecipient<OrderedMessage>,
    IRecipient<CartItemRemovedMessage>, IRecipient<UserExitedMessage>,
    IRecipient<CartItemQuantityChanged>
{
    private readonly IMessenger _messenger;
    private readonly List<CartItem> _items;

    public CartStore(IMessenger messenger)
    {
        _messenger = messenger;
        _items = new();
        
        messenger.RegisterAll(this);
    }

    public void AddToCart(CartItem cartItem)
    {
        var item = _items.Find(i => i.Id == cartItem.Id);
        if (item != null)
        {
            item.Quantity++;
            return;
        }
        _items.Add(cartItem);
        _messenger.Send(new CartItemAddedMessage(cartItem));
    }

    public void Receive(CartItemsRequestMessage message)
    {
        message.Reply(_items);
    }

    public void Receive(OrderedMessage message)
    {
        _items.Clear();
    }

    public void Receive(CartItemRemovedMessage message)
    {
        var item = _items.Find(i => i.Id == message.ItemId);
        
        if (item == null) return;
        
        _items.Remove(item);
    }

    public void Receive(UserExitedMessage message)
    {
        _items.Clear();
    }

    public void Receive(CartItemQuantityChanged message)
    {
        var item = _items.Find(i => i.Id == message.Id);

        if (item == null) return;

        item.Quantity = message.Quantity;
    }
}