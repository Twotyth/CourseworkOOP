namespace Domain.Entities;

public class Prescription
{
    public Prescription(uint id, IEnumerable<Medicine> prescribed)
    {
        Id = id;
        Prescribed = prescribed;
    }

    public uint Id { get; }
    public IEnumerable<Medicine> Prescribed { get; }
}