using System.Collections;
using DataLayer.Dtos;
using DataLayer.Exceptions;
using Domain.Entities;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DataLayer.Repositories;

public sealed class ProductRepos : IRepos<Product, ProductDto>
{
    private List<Product>  _items;
    private readonly IRepos<Medicine, MedicineDto> _medicineRepos;

    public ProductRepos(IRepos<Medicine, MedicineDto> medicineRepos)
    {
        _medicineRepos = medicineRepos;
        _items = new();
    }

    private uint _lastId = 0;

    public void Add(ProductDto item)
    {
        if (_items.Any(p => p.FullName == item.FullName && p.Manufacturer == item.Manufacturer))
        {
            throw new DuplicateException("Product with same name from same manufacturer already exists.");
        }
        
        _items.Add(new Product(
                ++_lastId, item.Name, item.Manufacturer, item.Description, item.MedicineInfo == null 
                    ? null 
                    : new MedicineInfo(
                        _medicineRepos.Find(item.MedicineInfo.MedicineId) 
                        ?? throw new ArgumentException($"Medicine of id {item.MedicineInfo} was not found."), 
                        item.MedicineInfo.Dosage, item.MedicineInfo.Quantity, item.MedicineInfo.DosageForm, item.MedicineInfo.QuantityForm, item.MedicineInfo.ConsumptionForm
                    ), 
                item.Price
            )
        );
    }

    public void Delete(Product item)
    {
        _items.Remove(item);
        OnRemoved?.Invoke(item);
    }

    public Product? Find(uint id) => _items.Find(i => i.Id == id);

    public Product? Find(Predicate<Product>  predicate) => _items.Find(predicate);
    
    public IEnumerable<Product> FindAll(Predicate<Product> predicate) => _items.FindAll(predicate);
    
    public void DeleteFirst(Predicate<Product>  predicate)
    {
        var item = Find(predicate);
        if (item == null) return;

        Delete(item);
    }

    public event Action<Product>? OnRemoved;

    public string Serialize() => JsonConvert.SerializeObject(
        _items.Select(
            p => new ProductDto(p.Id, p.Name, p.Manufacturer, p.Description, 
                p.MedicineInfo == null 
                    ? null 
                    : new MedicineInfoDto(p.MedicineInfo.Medicine.Id, p.MedicineInfo.Dosage, p.MedicineInfo.Quantity, p.MedicineInfo.DosageForm, p.MedicineInfo.QuantityForm, p.MedicineInfo.ConsumptionForm), 
                p.Price, p.IsOnSale)
        ).ToArray()
    );

    public void Deserialize(string json)
    {
        var deserializedItems = JsonConvert.DeserializeObject<ProductDto[]>(json)!
            .Select(pdto => new Product(
                pdto.Id, pdto.Name, pdto.Manufacturer, pdto.Description,
                pdto.MedicineInfo == null 
                    ? null 
                    : new MedicineInfo(
                        _medicineRepos.Find(pdto.MedicineInfo.MedicineId) ?? throw new ArgumentException($"Medicine of id {pdto.MedicineInfo} was not found."), 
                        pdto.MedicineInfo.Dosage, pdto.MedicineInfo.Quantity, pdto.MedicineInfo.DosageForm, pdto.MedicineInfo.QuantityForm, pdto.MedicineInfo.ConsumptionForm
                    ),
                pdto.Price
                )
            )
            .ToList();

        _items = deserializedItems;

        if (!_items.Any()) return;
        
        _lastId = _items.Max(i => i.Id);
    }

    public IEnumerator<Product>  GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}