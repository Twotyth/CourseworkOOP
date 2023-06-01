namespace Domain.Entities;

public sealed class MedicineInfo
{
    public MedicineInfo(
        Medicine medicine, uint dosage, uint quantity,
        DosageForm dosageForm, QuantityForm quantityForm, ConsumptionForm consumptionForm)
    {
        Medicine = medicine;
        Dosage = dosage;
        Quantity = quantity;
        DosageForm = dosageForm;
        QuantityForm = quantityForm;
        ConsumptionForm = consumptionForm;
    }
    public Medicine Medicine { get; }
    public uint Dosage { get; }
    public uint Quantity { get; }
    public DosageForm DosageForm { get; }
    public QuantityForm QuantityForm { get; }
    public ConsumptionForm ConsumptionForm { get; }
}

public enum QuantityForm
{
    Pieces,
    Milliliters,
    Grams,
}

public enum ConsumptionForm
{
    Oral,
    Ophtalmic,
    Injection,
    Inhalation,
    Other
}

public enum DosageForm
{
    Mg, 
    Ml,
    G
}