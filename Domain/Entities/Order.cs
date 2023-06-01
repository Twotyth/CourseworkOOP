using System.Collections.Immutable;
using Domain.Common;
using Domain.Entities.Users;

namespace Domain.Entities;

public class Order
{
    private Client? _client;

    public Order(
        uint id, Client? client, IDictionary<Product, uint> ordered, Pharmacy deliveryPharmacy, 
        bool prescriptionRequired, decimal discount)
    {
        Id = id;
        Status = OrderStatus.Delivering;
        Ordered = ordered.ToImmutableDictionary();
        DeliveryPharmacy = deliveryPharmacy;
        PrescriptionRequired = prescriptionRequired;
        Discount = discount;
        TotalPrice = Ordered.Sum(i => i.Key.Price*i.Value) - discount;
        _client = client;
        OrderDate = DateOnly.FromDateTime(DateTime.Today);
    }

    public uint Id { get; }

    public Client? Client
    {
        get => _client;
        set
        {
            if (value != null) 
                throw new ArgumentException("Cannot change clien to other client");

            _client = null;
        }
    }

    public OrderStatus Status { get; set; }

    public DateOnly OrderDate { get; init; }

    public IImmutableDictionary<Product, uint> Ordered { get; }

    public Pharmacy DeliveryPharmacy { get; }

    public bool PrescriptionRequired { get; }

    public decimal Discount { get; }

    public decimal TotalPrice { get; }
}