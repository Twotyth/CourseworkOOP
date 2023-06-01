using Domain.Entities;

namespace Application.InfoObjects;

public record ProductInfo(uint Id, string Name, string Manufacturer, string Description, bool IsOnSale,
    ProductMedicineInfo? MedicineInfo, decimal Price)
{
    public string FullName => $"{Name}, {MedicineInfo?.Dosage} " +
                              $"{(MedicineInfo?.DosageForm == null ? null : DosageQuantityConverter(MedicineInfo.DosageForm))} " +
                              $"{MedicineInfo?.Quantity} {(MedicineInfo?.QuantityForm == null ? null : QuantityConverter(MedicineInfo.QuantityForm))}";

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
};

public record ProductMedicineInfo(uint MedicineId, uint Dosage, uint Quantity,
    DosageForm DosageForm, QuantityForm QuantityForm, ConsumptionForm ConsumptionForm);