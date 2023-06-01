namespace Domain.Entities;

public class Product
{
    private string _name;
    private string _manufacturer;
    private decimal _price;
    private string _description;

    public Product(
        uint id, string name, string manufacturer, string description, 
        MedicineInfo? medicineInfo, decimal price, bool isOnSale = true
        )
    {
        Id = id;
        Name = name;
        Manufacturer = manufacturer;
        Description = description;
        MedicineInfo = medicineInfo;
        Price = price;
        IsOnSale = isOnSale;
    }

    public uint Id { get; }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrEmpty(value.Trim()))
            {
                throw new ArgumentException(
                    "Name for product must be not empty or whitespace"
                );
            }
            _name = value;
        }
    }
    
    public string FullName => $"{Name}, {MedicineInfo?.Dosage} " +
                              $"{(MedicineInfo?.DosageForm == null ? null : DosageQuantityConverter(MedicineInfo.DosageForm))} " +
                              $"{MedicineInfo?.Quantity} {(MedicineInfo?.QuantityForm == null ? null : QuantityConverter(MedicineInfo.QuantityForm))}";

    public string Manufacturer
    {
        get => _manufacturer;
        set
        {
            if (string.IsNullOrEmpty(value.Trim()))
            {
                throw new ArgumentException("Manufacturer must be not empty or whitespace");
            }
            _manufacturer = value;
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            if (string.IsNullOrEmpty(value.Trim()))
            {
                throw new ArgumentException("Description must not be empty or whitespace");
            }
            _description = value;
        }
    }

    public decimal Price
    {
        get => _price;
        set
        {
            var val = decimal.Round(value, 2, MidpointRounding.ToZero);
            if (val <= 0)
            {
                throw new ArgumentException("Price must be positive value");
            }
            _price = val;
        }
    }
    
    public bool IsOnSale { get; set; }
    

    public MedicineInfo? MedicineInfo { get; set; }
    
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