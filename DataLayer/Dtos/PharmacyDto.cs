namespace DataLayer.Dtos;

public class PharmacyDto
{
    public PharmacyDto(
        uint id, string address, IDictionary<uint, uint> medicineIdStock, uint? workingPharmacistId, 
        IDictionary<uint, (DateOnly, decimal, bool)> orderPackages
        )
    {
        Id = id;
        Address = address;
        MedicineIdStock = medicineIdStock;
        WorkingPharmacistId = workingPharmacistId;
        OrderPackages = orderPackages;
    }

    public uint Id { get; set; }
    public string Address { get; set; }
    public IDictionary<uint, uint> MedicineIdStock { get; set; }
    public IDictionary<uint, (DateOnly, decimal, bool)> OrderPackages { get; set; }
    public uint? WorkingPharmacistId { get; set; }
}