namespace Domain.Entities.Users;

public class Pharmacist : RegisteredUser
{
    public uint Salary { get; set; }
    public Pharmacist(uint id, string login, string password, uint salary) : base(id, login, password)
    {
        Salary = salary;
    }
}