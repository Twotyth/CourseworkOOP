namespace DataLayer.Dtos;

public class MedicineDto 
{
    public uint Id { get; set; }
    public MedicineDto(uint id, string name, bool isPrescriptionRequired, string type, string typeCategory)
    {
        Name = name;
        IsPrescriptionRequired = isPrescriptionRequired;
        Type = type;
        TypeCategory = typeCategory;
        Id = id;
    }

    public string Name { get; set; }
    public bool IsPrescriptionRequired { get; set; }
    
    public string Type { get; set; }
    public string TypeCategory { get; set; }
}