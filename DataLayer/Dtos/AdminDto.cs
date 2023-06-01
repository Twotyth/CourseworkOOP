namespace DataLayer.Dtos;

public class AdminDto : UserDto
{
    public AdminDto(uint id, string login, string password) : base(id, login, password)
    {
    }

    public override UserType Type => UserType.Admin;
}