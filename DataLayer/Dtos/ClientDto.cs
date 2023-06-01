namespace DataLayer.Dtos;

public class ClientDto : UserDto
{
    public decimal Balance { get; set; }

    public ClientDto(uint id, string login, string password, decimal balance) : base(id, login, password)
    {
        Balance = balance;
    }

    public override UserType Type => UserType.Client;
}