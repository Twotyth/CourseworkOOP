namespace DataLayer.Dtos;

public class PharmacistDto : UserDto
{
    public uint Salary { get; set; }
    
    public PharmacistDto(uint id, string login, string password, uint salary) : base(id, login, password)
    {
        Salary = salary;
    }

    public override UserType Type => UserType.Pharmacist;
}