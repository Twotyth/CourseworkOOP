using System.Collections;
using DataLayer.Dtos;
using DataLayer.Exceptions;
using Domain.Common;
using Domain.Entities;
using Newtonsoft.Json;

namespace DataLayer.Repositories;

public sealed class LocalMedicineRepos : IRepos<Medicine, MedicineDto>
{
    private readonly string _filepath;
    private List<Medicine>  Items
    {
        get
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Medicine>>(File.ReadAllText(_filepath))!;
            }
            catch
            {
                return new List<Medicine>();
            }
        }
    }

    public LocalMedicineRepos(string jsonPath)
    {
        _filepath = Path.Combine(jsonPath, "medicine_repos.json");
    }

    private uint LastId => Items.Max(i => i.Id);

    public void Add(MedicineDto item)
    {
        var items = Items;
        if (items.Any(med => med.Name == item.Name))
        {
            throw new DuplicateException($"Medicine of name {item.Name} already exists.");
        }
        items.Add(new(LastId + 1, item.Name, item.IsPrescriptionRequired, item.Type, item.TypeCategory));
        Override(items);
    }

    public void Delete(Medicine item)
    {
        var items = Items;
        if (items.IndexOf(item) == -1) return;
        items.Remove(item);
        Override(items);
        OnRemoved?.Invoke(item);
    }

    public Medicine? Find(uint id) => Items.Find(i => i.Id == id);

    public Medicine? Find(Predicate<Medicine>  predicate) => Items.Find(predicate);
    public IEnumerable<Medicine> FindAll(Predicate<Medicine> predicate) => Items.FindAll(predicate);

    public void DeleteFirst(Predicate<Medicine>  predicate)
    {
        var item = Find(predicate);
        if (item == null) return;

        Delete(item);
    }

    public event Action<Medicine>? OnRemoved;

    public string Serialize() => throw new NotSupportedException();

    public void Deserialize(string json) => throw new NotSupportedException();

    private void Override(List<Medicine> newValue) 
        => File.WriteAllText(_filepath, JsonConvert.SerializeObject(newValue)
    );

    public IEnumerator<Medicine>  GetEnumerator() => Items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    // private sealed class MedicineJsonConverter : JsonConverter<Medicine>
    // {
    //     public override bool CanConvert(Type typeToConvert) => typeof(Medicine).IsAssignableFrom(typeToConvert);
    //
    //     public override Medicine? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //     {
    //         throw new NotImplementedException();
    //     }
    //
    //     public override void Write(Utf8JsonWriter writer, Medicine value, JsonSerializerOptions options)
    //     {
    //         throw new NotImplementedException();
    //     }
    // }
}