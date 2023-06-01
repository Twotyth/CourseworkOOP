using System.Collections;
using System.Text.Json;
using DataLayer.Dtos;
using Domain.Entities;

namespace DataLayer.Repositories;

public sealed class LocalPrescriptionRepos : IRepos<Prescription, PrescriptionDto>
{
    private List<Prescription> Items => Prescriptions();


    private readonly IRepos<Medicine, MedicineDto> _medicineRepos;

    public LocalPrescriptionRepos(IRepos<Medicine, MedicineDto> medicines, string jsonPath)
    {
        _medicineRepos = medicines;
        _filepath = Path.Combine(jsonPath, "prescription_repos.json");
    }

    private uint LastId => Items.Max(i => i.Id);
    private readonly string _filepath;
    private List<Prescription> Prescriptions()
    {
        try
        {
            return JsonSerializer.Deserialize<List<PrescriptionDto>>(File.ReadAllText(_filepath))!.Select(i =>
                    new Prescription(i.Id,
                        i.PrescribedMedicineId.Select(medId => _medicineRepos.Find(medId)!)))
                .ToList();
        }
        catch
        {
            return new();
        }
    }

    public void Add(PrescriptionDto item)
    {
        var prescribed = item.PrescribedMedicineId
            .Select(medicineId => 
                _medicineRepos.Find(medicineId)
                ?? throw new ArgumentException($"Medicine of id {medicineId} to prescribe was not found")
            );

        var items = Items;
        uint i = 0;
        if (Items.Any())
        {
            i = LastId;
        }
        
        items.Add(new(i+1, prescribed));
        Override(items);
    }

    public void Delete(Prescription item)
    {
        var items = Items;
        if (items.All(i => i.Id != item.Id)) return;
        items.RemoveAt(items.FindIndex(i => i.Id == item.Id)); 
        Override(items);
        OnRemoved?.Invoke(item);
    }

    public Prescription? Find(uint id) => Items.Find(i => i.Id == id);

    public Prescription? Find(Predicate<Prescription>  predicate) => Items.Find(predicate);
    
    public IEnumerable<Prescription> FindAll(Predicate<Prescription> predicate) => Items.FindAll(predicate);
    
    public void DeleteFirst(Predicate<Prescription>  predicate)
    {
        var item = Find(predicate);
        if (item == null) return;

        Delete(item);
    }

    private void Override(List<Prescription> newValue) => File.WriteAllText(
        _filepath, JsonSerializer.Serialize(newValue.Select(
            i => new PrescriptionDto(
                i.Id, i.Prescribed.Select(p => p.Id)
            ))
            .ToList()
        )
    );
    
    public void Deserialize(string jsonString) => throw new NotSupportedException();

    public string Serialize() => throw new NotSupportedException();

    public event Action<Prescription>? OnRemoved;

    public IEnumerator<Prescription>  GetEnumerator() => Items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}