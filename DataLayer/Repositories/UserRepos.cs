using System.Collections;
using DataLayer.Dtos;
using DataLayer.Exceptions;
using Domain.Entities.Users;
using Newtonsoft.Json;

namespace DataLayer.Repositories;

public sealed class UserRepos : IRepos<RegisteredUser, UserDto>
{
    private uint _lastId = 0;
    
    private List<RegisteredUser>  _items;

    public UserRepos()
    {
        _items = new();
    }

    public void Add(UserDto item)
    {
        if (_items.Any(x => x.Login == item.Login))
        {
            throw new DuplicateException("User with that login already exists.");
        }
        
        try
        {
            switch (item)
            {
                case ClientDto clientDto:
                    _items.Add(new Client(++_lastId, clientDto.Login, clientDto.Password!));
                    break;

                case PharmacistDto pharmacistDto:
                    _items.Add(new Pharmacist(
                            ++_lastId, pharmacistDto.Password!, pharmacistDto.Password!, pharmacistDto.Salary
                        )
                    );
                    break;

                case AdminDto:
                    _items.Add(new Admin(++_lastId, item.Login, item.Password!));
                    break;

                default:
                    throw new Exception();
            }
        }
        catch (Exception e)
        {
            throw new ArgumentException($"Provided user credentials were not valid: {e.Message}");
        }
    }

    public void Delete(RegisteredUser item)
    {
        var i = _items.IndexOf(item);
        if (i == -1) return;

        if (!_items.Remove(item)) return;
        
        OnRemoved?.Invoke(item);
    }

    public RegisteredUser? Find(uint id) => _items.Find(i => i.Id == id);

    public RegisteredUser? Find(Predicate<RegisteredUser>  predicate) => _items.Find(predicate);

    public IEnumerable<RegisteredUser> FindAll(Predicate<RegisteredUser> predicate) => _items.FindAll(predicate);
    
    public void DeleteFirst(Predicate<RegisteredUser>  predicate)
    {
        var item = Find(predicate);
        if (item == null) return;

        Delete(item);
    }

    public event Action<RegisteredUser>? OnRemoved;
    
    public string Serialize()
    {
        var s = new StringWriter();
        
        var serializer = JsonSerializer.Create();
        serializer.Serialize(
            s,
            _items.Select<RegisteredUser, UserDto>( user => user switch
                {
                    Admin admin => new AdminDto(admin.Id, admin.Login, admin.Password),
                    Client client => new ClientDto(client.Id, client.Login, client.Password, client.Balance),
                    Pharmacist pharmacist => new PharmacistDto(pharmacist.Id, pharmacist.Login, pharmacist.Password,
                        pharmacist.Salary),
                    _ => throw new ArgumentOutOfRangeException(nameof(user))
                }).ToArray()
        );

        return s.ToString();
    }

    public void Deserialize(string json)
    {
        
        var deserializedItems = JsonConvert.DeserializeObject<UserDto[]>(json)!
            .Select<UserDto, RegisteredUser>(user => user switch
                {
                    ClientDto clientDto
                        => new Client(
                            clientDto.Id, clientDto.Login, 
                            clientDto.Password!
                        ){Balance = clientDto.Balance},

                    PharmacistDto pharmacistDto
                        => new Pharmacist(
                            pharmacistDto.Id, pharmacistDto.Login!,
                            pharmacistDto.Password!, pharmacistDto.Salary
                        ),

                    AdminDto =>
                        new Admin(user.Id, user.Login, user.Password!),
                    _ =>
                        throw new ArgumentOutOfRangeException(nameof(user))
                }
            ).ToList();

        _items = deserializedItems;

        if (!_items.Any()) return;
        
        _lastId = _items.Max(i => i.Id);
    }

    public IEnumerator<RegisteredUser>  GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

