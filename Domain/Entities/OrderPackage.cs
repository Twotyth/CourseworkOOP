using System.Collections.Immutable;

namespace Domain.Entities;

public class OrderPackage
{
    public uint OrderId { get; }
    public DateOnly ArrivalDate { get; }
    public decimal Discount { get; }
    public bool IsPrescriptionRequired { get; }
    
    public OrderPackage(Order order)
    {
        OrderId = order.Id;
        ArrivalDate = DateOnly.FromDateTime(DateTime.Today);
        Discount = order.Discount;
        IsPrescriptionRequired = order.PrescriptionRequired;
    }

    public OrderPackage(uint orderId, DateOnly arrivalDate, decimal discount, bool isPrescriptionRequired)
    {
        OrderId = orderId;
        ArrivalDate = arrivalDate;
        Discount = discount;
        IsPrescriptionRequired = isPrescriptionRequired;
    }
}