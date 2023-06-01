using System.Collections;
using DataLayer.Dtos;
using Domain.Entities;
using Domain.Entities.Users;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DataLayer.Repositories;

public sealed class OrderRepos : IRepos<Order, OrderDto>
{
    private List<Order>  _items;
    private readonly IRepos<Product, ProductDto> _products;
    private readonly IRepos<RegisteredUser, UserDto> _userRepos;
    private readonly IRepos<Pharmacy, PharmacyDto> _pharmacies;
    private uint _lastId = 0;

    public OrderRepos(
        IRepos<Product, ProductDto> productRepos, 
        IRepos<Pharmacy, PharmacyDto> pharmacies, 
        IRepos<RegisteredUser, UserDto> userRepos
    )
    {
        _items = new();
        productRepos.OnRemoved += product =>
        {
            if (_items.Any(x => x.Ordered.ContainsKey(product)))
                throw new OperationCanceledException(
                    "Before removing medicine product system, all orders with" +
                    " it must be delivered or canceled." +
                    "\nConsider restricting its purchase first.");
        };

        userRepos.OnRemoved += user =>
        {
            if (user is not Client) return;
            
            foreach (var order in _items.Where(order => order.Client == user))
            {
                order.Client = null;
            }
        };
        
        _products = productRepos;
        _pharmacies = pharmacies;
        _userRepos = userRepos;
    }


    public void Add(OrderDto item)
    {


        Client? client = null;
        

        if (item.ClientId != null)
        {
            client = _userRepos.Find(item.ClientId.Value) as Client 
                   ?? throw new ArgumentException($"Provided client id of {item.ClientId} was not id of pharmacy chain client.");
        }

        var ordered = item.OrderedProductsIdsQuantity
            .ToDictionary(pIdQuantity =>
                _products.Find(pIdQuantity.Key) ?? throw new ArgumentException($"Product to order of id {pIdQuantity} was not found."), 
                pIdQuantity => pIdQuantity.Value
                );

        var deliveryPharmacy = _pharmacies
            .Find(p => p.Id == item.DeliveryPharmacyId)
            ?? throw new ArgumentException($"Pharmacy to deliver to of address {item.DeliveryPharmacyId} was not found.");

        if (deliveryPharmacy.WorkingPharmacist == null)
        {
            throw new ArgumentException(
                $"Cannot add order to pharmacy of id {deliveryPharmacy.Id}: no pharmacist working there to accept delivery");
        }
        
        _items.Add(new Order(++_lastId, client, ordered, deliveryPharmacy, item.PrescriptionRequired, item.Discount));
    }

    public void Delete(Order item)
    {
        if (_items.IndexOf(item) == -1) return;
        _items.Remove(item);
        OnRemoved?.Invoke(item);
    }

    public Order? Find(uint id) => _items.Find(i => i.Id == id);


    public Order? Find(Predicate<Order>  predicate) => _items.Find(predicate);
    
    public IEnumerable<Order> FindAll(Predicate<Order> predicate) => _items.FindAll(predicate);
    
    public void DeleteFirst(Predicate<Order>  predicate)
    {
        var item = Find(predicate);
        if (item == null) return;

        Delete(item);
    }
    
    public event Action<Order>? OnRemoved;
    
    public string Serialize() 
        => JsonConvert.SerializeObject(
            _items.Select(o 
                => new OrderDto(o.Id, o.Client?.Id, o.Status, o.OrderDate, o.DeliveryPharmacy.Id, 
                    o.TotalPrice, o.Discount ,
                    o.Ordered.ToDictionary(pq => pq.Key.Id, pq => pq.Value),
                    o.PrescriptionRequired)
            ).ToArray()
        );

    public void Deserialize(string json)
    {
        var orderDtoArr = JsonConvert.DeserializeObject<OrderDto[]>(json)!;
        
        var deserializedItems  = orderDtoArr
            .Select(odto => new Order(
                odto.Id!.Value, odto.ClientId == null 
                    ? null 
                    : _userRepos.Find(odto.ClientId.Value) as Client
                      ?? throw new ArgumentException($"Client of id {odto.ClientId} was not found for order {odto.Id}."), 
                odto.OrderedProductsIdsQuantity
                    .ToDictionary(
                        opiq => _products.Find(opiq.Key) 
                                ?? throw new ArgumentException($"Product of id {opiq.Key} was not found for order {odto.Id}."), 
                        opiq => opiq.Value
                    ), 
                _pharmacies.Find(ph => ph.Id == odto.DeliveryPharmacyId) 
                    ?? throw new ArgumentException($"Pharmacy of address {odto.DeliveryPharmacyId} was not found for order {odto.Id}"), 
                odto.PrescriptionRequired, odto.Discount
                )
                {
                    Status = odto.Status,
                    OrderDate = odto.OrderDate
                }
            ).ToList();

        _items = deserializedItems;
        
        if (!_items.Any()) return;
        
        _lastId = _items.Max(i => i.Id);
    }

    public IEnumerator<Order>  GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}