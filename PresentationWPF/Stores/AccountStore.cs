using Application.Services.UserServices;

namespace PresentationWPF.Stores;

public class AccountStore
{
    internal IUserService? CurrentUser { get; set; } = null;

    public AccountStore()
    {
    }
}