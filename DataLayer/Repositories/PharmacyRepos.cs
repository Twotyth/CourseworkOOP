using System.Collections;
using DataLayer.Dtos;
using DataLayer.Exceptions;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Users;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DataLayer.Repositories;


public sealed class PharmacyRepos : IRepos<Pharmacy, PharmacyDto>
{
    private List<Pharmacy>  _items;
    private readonly IRepos<Product, ProductDto> _productRepos;
    private readonly IRepos<RegisteredUser, UserDto> _userReposes;
    private uint _lastId = 0;

    public PharmacyRepos(IRepos<RegisteredUser, UserDto> userRepos, IRepos<Product, ProductDto> productRepos)
    {
        _items = new();
        _userReposes = userRepos;
        _productRepos = productRepos;
    }


    public void Add(PharmacyDto item)
    {
        if (_items.Any(ph => ph.Address == item.Address))
        {
            throw new DuplicateException($"Pharmacy at address {item.Address} already exists.");
        }
        
        var workingPharmacist = _userReposes
                                    .Find(p => p.Id == item.WorkingPharmacistId) as Pharmacist
                                ?? throw new ArgumentException($"Pharmacist of id {item.WorkingPharmacistId} to work in pharmacy of address {item.Address} was not found.");

        if (_items.Any(ph => ph.WorkingPharmacist == workingPharmacist))
        {
            throw new ArgumentException(
                $"Pharmacist of id {item.WorkingPharmacistId} is already working at a pharmacy.");
        }
        
        _items.Add(new Pharmacy(++_lastId, item.Address, workingPharmacist));
    }

    public void Delete(Pharmacy item)
    {
        if (_items.IndexOf(item) == -1) return;
        _items.Remove(item);
        OnRemoved?.Invoke(item);
    }

    public Pharmacy? Find(uint id) => _items.Find(i => i.Id == id);


    public Pharmacy? Find(Predicate<Pharmacy>  predicate) => _items.Find(predicate);
    
    public IEnumerable<Pharmacy> FindAll(Predicate<Pharmacy> predicate) => _items.FindAll(predicate);
    
    public void DeleteFirst(Predicate<Pharmacy>  predicate)
    {
        var item = Find(predicate);
        if (item == null) return;

        Delete(item);
    }
    
    public event Action<Pharmacy>? OnRemoved;

    public string Serialize() => JsonConvert.SerializeObject(
        _items.Select(ph 
            => new PharmacyDto(
                ph.Id, ph.Address, 
                ph.Stock.ProductQuantity.ToDictionary(
                    pq => pq.Key.Id, 
                    pq => pq.Value
                    ), 
                ph.WorkingPharmacist?.Id, 
                ph.Stock.Orders.ToDictionary(
                    o => o.OrderId, 
                    o => (
                        o.ArrivalDate, o.Discount, 
                        o.IsPrescriptionRequired
                    )
                )
            )
        ).ToArray()
    );

    public void Deserialize(string json)
    {
        var deserializedItems = JsonConvert.DeserializeObject<PharmacyDto[]>(json)!
            .Select(phdto => new Pharmacy(
                phdto.Id, phdto.Address, phdto.WorkingPharmacistId == null 
                    ? null : _userReposes.Find(phdto.WorkingPharmacistId.Value) as Pharmacist 
                             ?? throw new ArgumentException($"Pharmacist of id {phdto.WorkingPharmacistId} to work in pharmacy of id {phdto.Id} was not found")
                )
                {
                    Stock = new ProductStock(
                        phdto.MedicineIdStock.Select(
                            pIdQ => (_productRepos.Find(pIdQ.Key) ?? throw new ArgumentException(""), pIdQ.Value)
                            
                        ),
                        phdto.OrderPackages.Select(
                            oId => new OrderPackage(oId.Key, oId.Value.Item1, oId.Value.Item2, oId.Value.Item3)
                        )
                    )
                }
            ).ToList();

        _items = deserializedItems;
        
        if (!_items.Any()) return;
        
        _lastId = _items.Max(i => i.Id);
    }

    public IEnumerator<Pharmacy>  GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
