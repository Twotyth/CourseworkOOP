namespace DataLayer.Dtos;

public class PrescriptionDto
{
    public PrescriptionDto(uint id, IEnumerable<uint> prescribedMedicineId)
    {
        Id = id;
        PrescribedMedicineId = prescribedMedicineId;
    }

    public uint Id { get; set; }
    public IEnumerable<uint> PrescribedMedicineId { get; set; }
}