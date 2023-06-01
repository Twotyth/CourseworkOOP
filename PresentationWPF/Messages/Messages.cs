using System;
using System.Collections.Generic;
using Application.InfoObjects;
using CommunityToolkit.Mvvm.Messaging.Messages;
using PresentationWPF.EntityItems.CartItem;
using PresentationWPF.Stores;
using PresentationWPF.ViewModels;

namespace PresentationWPF.Messages;


public class PharmacyAddressesRequestMessage : RequestMessage<IEnumerable<string>>
{
}
public class CartItemsRequestMessage : RequestMessage<IEnumerable<CartItem>>
{
}
public class OrderedMessage{}
public record CartItemAddedMessage(CartItem Item);

public record CartItemRemovedMessage(uint ItemId);

public record FilterSelectionChangedMessage(object Value, FilterList FilterList, FilterSelectionChangedAction Action);

public enum FilterSelectionChangedAction
{
    Deleted, Added
}

public enum FilterList
{
    Manufacturers,
    Types,
    Categories,
    Quantity
}

public record FiltersResetMessage();

public record CartItemQuantityChanged(uint Id, uint Quantity);

public record UserExitedMessage();
    
public class ManufacturerSelectionRequestMessage : RequestMessage<IEnumerable<string>>
{
} 

public class TypeSelectionRequestMessage : RequestMessage<IEnumerable<string>>
{
} 

public class CategorySelectionRequestMessage : RequestMessage<IEnumerable<string>>
{
} 

public class RangeSelectionRequestMessage : RequestMessage<(decimal, decimal)>
{
} 

public record ProductsChangedNotification;
public record PharmaciesChangedNotification;
public record PharmacistsChangedNotification;
public record OrdersChangedNotification;

public record EditProductMessage(ProductInfo ProductInfo);
public record EditPharmacyMessage(PharmacyInfo PharmacyInfo);
public record EditPharmacistMessage(PharmacistInfo PharmacistInfo);

public record SellProductsMessage;

public record CheckItemAddedMessage(CheckItem item);

public record CheckItemRemovedMessage(uint Id);

public record CheckItemQuantityChanged(uint Id, uint Quantity);

public class CheckItemsRequestMessage : RequestMessage<IEnumerable<CheckItem>>
{
    
}

public class StockOrderIdRequestMessage : RequestMessage<IEnumerable<uint>>
{
    
}

public record OrderAcceptedMessage(uint OrderId);
public record OrderGaveOutNotification;