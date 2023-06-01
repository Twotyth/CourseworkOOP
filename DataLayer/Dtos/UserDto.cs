using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace DataLayer.Dtos;


[JsonConverter(typeof(UserConverter))]
public abstract class UserDto
{
    public abstract UserType Type { get; }
    public uint Id { get; set; }
    public string Login { get; set; }
    public string? Password { get; set; }

    protected UserDto(uint id, string login, string? password)
    {
        Id = id;
        Login = login;
        Password = password;
    }
}

public enum UserType
{
    Client,
    Pharmacist,
    Admin
}

public class UserConverter : CustomCreationConverter<UserDto>
{
    private UserType _currentUserType;

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var jobj = JObject.ReadFrom(reader);
        _currentUserType = jobj["Type"].ToObject<UserType>();
        return base.ReadJson(jobj.CreateReader(), objectType, existingValue, serializer);
    }

    public override UserDto Create(Type objectType)
    {
        return _currentUserType switch
        {
            UserType.Client => new ClientDto(0, "", "", 0),
            UserType.Pharmacist => new PharmacistDto(0, "", "", 0),
            UserType.Admin => new AdminDto(0, "", ""),
            _ => throw new NotImplementedException()
        };
    }
}