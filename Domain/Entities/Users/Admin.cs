namespace Domain.Entities.Users;

public class Admin : RegisteredUser
{
    public Admin(uint id, string login, string password) : base(id, login, password)
    {
    }
}