using System.Data;
using Application.Enums;
using Application.Exceptions;
using Application.Services.UserServices;
using DataLayer.Dtos;
using DataLayer.Repositories;
using Domain.Entities.Users;

namespace Infrastructure.Services.UserServices;

public abstract class UserServiceBase<T> : IUserService where T : RegisteredUser
{
    protected IRepos<RegisteredUser, UserDto>? UserRepos;
    protected T? User;

    protected UserServiceBase(T user, IRepos<RegisteredUser, UserDto> userRepos)
    {
        User = user;
        UserRepos = userRepos;
    }
    
    public virtual void Exit()
    {
        User = null;
        UserRepos = null;
    }

    protected virtual void ValidateSession()
    {
        if (User == null || UserRepos == null)
        {
            throw new InvalidSessionException("Invalid session of user service.");
        }
    }

    public EditLoginResult EditLogin(string newLogin)
    {
        ValidateSession();
        
        try
        {
            User!.Login = newLogin;
        }
        catch (ArgumentException)
        {
            return EditLoginResult.DoesNotMeetReqs;
        }
        catch (DuplicateNameException)
        {
            return EditLoginResult.AlreadyExists;
        }
        
        
        return EditLoginResult.Success;
    }

    public EditPasswordResult EditPassword(string newPassword)
    {
        ValidateSession();

        try
        {
            User!.Password = newPassword;
        }
        catch (ArgumentException)
        {
            return EditPasswordResult.DoesNotMeetReqs;
        }

        return EditPasswordResult.Success;
    }
}

