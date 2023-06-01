using System.Text.RegularExpressions;

namespace Domain.Entities.Users;

public abstract class RegisteredUser
{
    private string _login;
    private string _password;

    protected RegisteredUser(uint id, string login, string password)
    {
        Id = id;
        Login = login;
        Password = password;
    }

    public uint Id { get; }

    public string Login
    {
        get => _login;
        set
        {
            if (!value.Any() || value.Length is > 256 or < 4)
                throw new ArgumentException(
                    "Login must be non-empty string that is less than 256 and more than 4 characters"
                );
            _login = value;
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            if (!Regex.IsMatch(value, 
                    "^(?=.*\\d)(?=.*[a-zA-Zа-яА-Я])(?=.*[!_\\-#@$%^&*()=\"';:.,`№?+=]).{8,256}$")
                )
            {
                throw new ArgumentException(
                    "Password must be non-empty string that contains 1 digit, 1 special symbol " +
                    "and is less than 256 and more than 6 characters");
            }
            
            _password = value;
        }
    }
}