using System;
using Application.Enums;
using Application.Services.UserServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DataLayer.Dtos;
using PresentationWPF.Messages;
using PresentationWPF.Stores;

namespace PresentationWPF.ViewModels;

public partial class AdminPharmacistViewModel : PopupViewModelBase, 
    IRecipient<UserExitedMessage>
{
    private readonly IMessenger _messenger;
    private IAdminService? _adminService;

    [ObservableProperty]
    private bool isEditing = false;

    public bool IsAdding => !IsEditing;
    public uint? Id { get; set; }

    public string Login { get; set; } = "";
    public string Password { get; set; } = "";
    public string Salary { get; set; } = "";

    [ObservableProperty]
    private string errorMessage = "";

    public AdminPharmacistViewModel(IMessenger messenger, MainViewModel mvm, AccountStore accountStore) : base(mvm)
    {
        _messenger = messenger;
        
        _adminService = accountStore.CurrentUser as IAdminService ?? throw new ArgumentException("Current user was not expected. Expected user: Admin");
    }
    
    [RelayCommand]
    private void Edit()
    {
        if (!IsEditing)
        {
            return;
        }
        if (Id == null)
        {
            ErrorMessage = "Pharmacist id is not valid.";
            return;
        }

        if (!uint.TryParse(Salary, out var salary))
        {
            ErrorMessage = "Provided salary was not valid";
            return;
        }
        var result = _adminService!.EditPharmacist(Id.Value, salary);

        ErrorMessage = result switch
        {
            EditPharmacistResult.Success => "",
            EditPharmacistResult.PharmacistNotFount => "Pharmacist was not found",
            EditPharmacistResult.Failed => "Edit failed",
            _ => throw new ArgumentOutOfRangeException()
        };

        if (result != EditPharmacistResult.Success) return;

        _messenger.Send<PharmacistsChangedNotification>();
        Close();
    }

    [RelayCommand]
    public void Add()
    {
        if (IsEditing)
        {
            return;
        }
        
        if (!uint.TryParse(Salary, out var salary))
        {
            ErrorMessage = "Provided salary was not valid";
            return;
        }
        var result = _adminService!.AddUser(new PharmacistDto(0, Login, Password, salary));

        ErrorMessage = result switch
        {
            AddUserResult.Success => "",
            AddUserResult.UserAlreadyExists => "User with that login already exists",
            AddUserResult.PropsDoNotMeetReqs => "Properties do not meet requirements",
            _ => throw new ArgumentOutOfRangeException()
        };

        if (result != AddUserResult.Success) return;

        _messenger.Send<PharmacistsChangedNotification>();
        Close();
    }
    

    public void Receive(UserExitedMessage message)
    {
        _adminService = null;
    }

}