using Domain.Entities;

namespace DataLayer.Dtos;

public sealed class ProductDto
{
    public ProductDto(uint id, string name, string manufacturer, string description, MedicineInfoDto? medicineInfo, decimal price, bool isOnSale)
    {
        Id = id;
        Name = name;
        Manufacturer = manufacturer;
        Description = description;
        MedicineInfo = medicineInfo;
        Price = price;
        IsOnSale = isOnSale;
    }
    public uint Id { get; set; }
    public string Name { get; set; }
    public string FullName => $"{Name}, {MedicineInfo?.Dosage} " +
                              $"{(MedicineInfo?.DosageForm == null ? null : DosageQuantityConverter(MedicineInfo.DosageForm))} " +
                              $"{MedicineInfo?.Quantity} {(MedicineInfo?.QuantityForm == null ? null : QuantityConverter(MedicineInfo.QuantityForm))}";
    public string Manufacturer { get; set; }
    public string Description { get; set; }
    public MedicineInfoDto? MedicineInfo { get; set; }
    public decimal Price { get; set; }
    
    public bool IsOnSale { get; set; }
    
    private string QuantityConverter(QuantityForm medicineInfoQuantityForm) => medicineInfoQuantityForm switch
    {
        QuantityForm.Pieces => "pcs",
        QuantityForm.Milliliters => "ml",
        QuantityForm.Grams => "g",
        _ => throw new ArgumentOutOfRangeException(nameof(medicineInfoQuantityForm), medicineInfoQuantityForm, null)
    };

    private string DosageQuantityConverter(DosageForm dosageForm) => dosageForm switch
    {
        DosageForm.Mg => "mg",
        DosageForm.Ml => "ml",
        DosageForm.G => "g",
        _ => throw new ArgumentOutOfRangeException(nameof(dosageForm), dosageForm, null)
    };
}

public sealed class MedicineInfoDto
{
    public MedicineInfoDto(uint medicineIdId, uint dosage, uint quantity, DosageForm dosageForm,
        QuantityForm quantityForm, ConsumptionForm consumptionForm)
    {
        MedicineId = medicineIdId;
        Dosage = dosage;
        Quantity = quantity;
        DosageForm = dosageForm;
        QuantityForm = quantityForm;
        ConsumptionForm = consumptionForm;
    }
    public uint MedicineId { get; set; }
    public uint Dosage { get; set; }
    public uint Quantity { get; set; }
    public DosageForm DosageForm { get; set; }
    public QuantityForm QuantityForm { get; set; }
    public ConsumptionForm ConsumptionForm { get; set; }
}