using Application.Enums;
using Domain.Entities;

namespace Application.Services.UserServices;

public interface IUserService
{
    public void Exit();
    public EditLoginResult EditLogin(string newLogin);
    public EditPasswordResult EditPassword(string newPassword);
}