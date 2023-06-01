using Domain.Common;

namespace DataLayer.Dtos;

public class OrderDto
{
    public OrderDto(
        uint? id, uint? clientId, OrderStatus status, DateOnly orderDate, uint deliveryPharmacyId, 
        decimal totalPrice, decimal discount,
        IDictionary<uint, uint> orderedProductsIdsQuantity, bool prescriptionRequired
        )
    {
        Id = id;
        ClientId = clientId;
        Status = status;
        DeliveryPharmacyId = deliveryPharmacyId;
        TotalPrice = totalPrice;
        Discount = discount;
        OrderedProductsIdsQuantity = orderedProductsIdsQuantity;
        PrescriptionRequired = prescriptionRequired;

        OrderDate = orderDate;
    }
    public uint? Id { get; set; }
    public uint? ClientId { get; set; }
    
    public IDictionary<uint, uint> OrderedProductsIdsQuantity { get; set; }
    public OrderStatus Status { get; set; }
    public DateOnly OrderDate { get; set; }
    public uint DeliveryPharmacyId { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Discount { get; set; }
    public bool PrescriptionRequired { get; set; }
}