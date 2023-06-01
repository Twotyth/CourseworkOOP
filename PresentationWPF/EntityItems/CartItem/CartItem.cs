namespace PresentationWPF.EntityItems.CartItem;

public class CartItem
{
    public uint Id { get; }
    public uint Quantity { get; set; }
    public string Name { get; }
    public string Manufacturer { get; }
    public bool IsPrescriptionRequired { get; }
    public decimal Price { get; }

    public CartItem(uint id, uint quantity, string name, string manufacturer, decimal price)
    {
        Id = id;
        Quantity = quantity;
        Name = name;
        Manufacturer = manufacturer;
        Price = price;
    }
}