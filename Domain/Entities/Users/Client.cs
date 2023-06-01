namespace Domain.Entities.Users;

public class Client : RegisteredUser
{
    public decimal Balance { get; set; }

    public Client(uint id, string login, string password) : base(id, login, password)
    {
        Balance = 0;
    }
}