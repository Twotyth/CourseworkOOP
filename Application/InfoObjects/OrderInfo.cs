using Domain.Common;

namespace Application.InfoObjects;

public record OrderInfo(
    uint Id, uint? ClientId, OrderStatus Status, DateOnly OrderDate,
    IDictionary<string, uint> ProductNamesQuantity,
    decimal TotalPrice,
    uint DeliveryPharmacyId, bool PrescriptionRequired
);