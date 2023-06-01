using System.Collections;
using Domain.Entities;

namespace Domain.Common;

public class ProductStock : IEnumerable<Product>
{
    public Dictionary<Product, uint> ProductQuantity { get; }
    public List<OrderPackage> Orders { get; }
    
    public ProductStock(IEnumerable<(Product, uint)>? productsFrom = null, IEnumerable<OrderPackage>? ordersFrom = null)
    {
        ProductQuantity = productsFrom == null ? new Dictionary<Product, uint>() : productsFrom.ToDictionary(x => x.Item1, x => x.Item2);

        Orders = ordersFrom == null ? new List<OrderPackage>() : new(ordersFrom);
    }
    public void Add(Product product, uint quantity)
    {
        if (ProductQuantity.ContainsKey(product))
        {
            ProductQuantity[product] += quantity;
            return;
        }
        
        ProductQuantity[product] = quantity;
    }

    public void AddOrder(Order order) => Orders.Add(new OrderPackage(order));
    
    public void Remove(Product product, uint quantity)
    {
        if (!ProductQuantity.ContainsKey(product)) return;
        
        var count = ProductQuantity[product] - quantity;
        
        if (count <= 0)
        {
            ProductQuantity.Remove(product);
            return;
        }

        ProductQuantity[product] -= quantity;
    }
    
    public void RemoveOrder(uint orderId)
    {
        var order = Orders.Find(o => o.OrderId == orderId) 
                    ?? throw new ArgumentException($"Order of id {orderId} was not found in stock");
        
        Orders.Remove(order);
    }

    public bool HasOrder(uint orderId) => Orders.Any(o => o.OrderId == orderId);

    IEnumerator<Product> IEnumerable<Product>.GetEnumerator() => ProductQuantity.Keys.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}