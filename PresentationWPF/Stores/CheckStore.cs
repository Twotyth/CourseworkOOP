using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Application.InfoObjects;
using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using PresentationWPF.Messages;
using PresentationWPF.ViewModels;

namespace PresentationWPF.Stores;

public sealed class CheckStore : 
    IRecipient<SellProductsMessage>, IRecipient<CheckItemsRequestMessage>,
    IRecipient<CheckItemRemovedMessage>, IRecipient<CheckItemQuantityChanged>
{
    private readonly IMessenger _messenger;
    private List<CheckItem> _checkItems = new();

    public CheckStore(IMessenger messenger)
    {
        _messenger = messenger;
        messenger.RegisterAll(this);
    }

    public void AddToCheck(CheckItem checkItem)
    {
        var item = _checkItems.Find(i => i.Id == checkItem.Id);
        if (item != null)
        {
            item.Quantity++;
        } else _checkItems.Add(checkItem);
        _messenger.Send(new CheckItemAddedMessage(checkItem));
    }

    public void Receive(SellProductsMessage message)
    {
        _checkItems.Clear();
    }

    public void Receive(CheckItemsRequestMessage message)
    {
        message.Reply(_checkItems);
    }

    public void Receive(CheckItemRemovedMessage message)
    {
        var item = _checkItems.Find(i => i.Id == message.Id);
        
        if (item == null) return;

        _checkItems.Remove(item);
    }

    public void Receive(CheckItemQuantityChanged message)
    {
        var item = _checkItems.Find(i => i.Id == message.Id);
        
        if (item == null) return;

        item.Quantity = message.Quantity;
    }
}

public sealed class CheckItem
{
    public uint Id { get; }
    public uint Quantity { get; set; }
    public string Name { get; }
    public string Manufacturer { get; }
    public decimal Price { get; }

    public CheckItem(uint id, uint quantity, string name, string manufacturer, decimal price)
    {
        Id = id;
        Quantity = quantity;
        Name = name;
        Manufacturer = manufacturer;
        Price = price;
    }
}   