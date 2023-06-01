using System;
using System.Windows.Media;
using Application.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PresentationWPF.Messages;
using PresentationWPF.Stores;

namespace PresentationWPF.ViewModels;
// ReSharper disable InconsistentNaming

public partial class PersonalCabinetViewModel : ModalViewModelBase
{
    private readonly IMessenger _messenger;
    protected readonly AccountStore _accountStore;
    private readonly INavigationService _guestCatalogNavigation;

    public PersonalCabinetViewModel(
        IMessenger messenger,
        AccountStore accountStore, MainViewModel mvm,
        INavigationService guestCatalogNavigation
    ) : base(mvm)
    {
        if (accountStore.CurrentUser == null)
        {
            throw new ArgumentException("User must be sign in to app to access personal cabinet");
        }

        _messenger = messenger;
        _accountStore = accountStore;
        _guestCatalogNavigation = guestCatalogNavigation;
    }

    [ObservableProperty]
    private string newUsername = "";
    
    [ObservableProperty]
    private string newPassword = "";

    [ObservableProperty]
    private string newPasswordRepeat = "";

    [ObservableProperty]
    private string usernameMessage = "";

    [ObservableProperty]
    private string passwordMessage = "";

    [ObservableProperty]
    private SolidColorBrush? usernameMessageBrush;

    [ObservableProperty]
    private SolidColorBrush? passwordMessageBrush;

    [RelayCommand]
    private void ChangeUsername()
    {
        var editLoginResult = _accountStore.CurrentUser!.EditLogin(NewUsername);

        UsernameMessageBrush = editLoginResult == EditLoginResult.Success
            ? new SolidColorBrush(Colors.Green)
            : new SolidColorBrush(Colors.Red);

        if (editLoginResult == EditLoginResult.Success)
        {
            NewUsername = "";
        }
            
        
        UsernameMessage = editLoginResult switch
        {
            EditLoginResult.Success => "Username successfully changed!",
            EditLoginResult.AlreadyExists => "Given username already exists. Please think of another.",
            EditLoginResult.DoesNotMeetReqs => "Given username does not meet requirements: must be at least 4 and less than 256 characters long.",
            _ => throw new ArgumentOutOfRangeException()
        };
        
        
    }

    [RelayCommand]
    private void ChangePassword()
    {
        if (NewPassword != NewPasswordRepeat)
        {
            PasswordMessage = "Passwords don't match.";
            PasswordMessageBrush = new SolidColorBrush(Colors.Red);
            return;
        }


        var editPasswordResult = _accountStore.CurrentUser!.EditPassword(NewPassword);

        PasswordMessageBrush = editPasswordResult == EditPasswordResult.Success
            ? new SolidColorBrush(Colors.Green)
            : new SolidColorBrush(Colors.Red);

        if (editPasswordResult == EditPasswordResult.Success)
        {
            NewPassword = "";
            NewPasswordRepeat = "";
        }

        PasswordMessage = editPasswordResult switch
        {
            EditPasswordResult.Success => "Password successfully changed!",
            EditPasswordResult.DoesNotMeetReqs => 
                "Given password does not meet requirements: must be at least 8 and less than 256 characters long, contain 1 digit, 1 letter and 1 special character. Please try again.",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [RelayCommand]
    private void Exit()
    {
        _accountStore.CurrentUser!.Exit();
        _accountStore.CurrentUser = null;
        _messenger.Send<UserExitedMessage>();
        Close();
        _guestCatalogNavigation.Navigate();
    }
    
}