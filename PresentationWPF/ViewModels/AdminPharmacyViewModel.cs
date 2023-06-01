using System;
using System.Collections.Generic;
using System.Linq;
using Application.Enums;
using Application.InfoObjects;
using Application.Services;
using Application.Services.UserServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PresentationWPF.Messages;
using PresentationWPF.Stores;

namespace PresentationWPF.ViewModels;

public partial class AdminPharmacyViewModel : PopupViewModelBase, 
    IRecipient<UserExitedMessage>
{
    private readonly IMessenger _messenger;
    private IAdminService? _adminService;

    [ObservableProperty]
    private bool isEditing = false;

    public bool IsAdding => !IsEditing;

    public uint? Id { get; set; }
    
    [ObservableProperty]
    public string address;

    [ObservableProperty] 
    public uint? workingPharmacistId;

    public IEnumerable<uint> PharmacistsIds => ((IRetrieving<PharmacistInfo>)_adminService!).Get().Select(i => i.Id);

    [ObservableProperty]
    private string errorMessage = "";
    

    public AdminPharmacyViewModel(IMessenger messenger,MainViewModel mvm, AccountStore accountStore) : base(mvm)
    {
        _messenger = messenger;
        _adminService = accountStore.CurrentUser as IAdminService ??
                        throw new ArgumentException("Current user was not expected. Expected user: Admin");
    }
    
    [RelayCommand]
    private void Edit()
    {
        if (!IsEditing) return;
        
        if (Id == null)
        {
            ErrorMessage = "Provided id was not valid";
            return;
        }
        
        var result = _adminService!.EditPharmacy(Id.Value, Address, WorkingPharmacistId);

        ErrorMessage = result switch
        {
            EditPharmacyResult.PharmacyNotFound => "Pharmacy was not found",
            EditPharmacyResult.NewValueDoesNotMeetReqs => "New values do not meet requirements",
            EditPharmacyResult.Success => "",
            _ => throw new ArgumentOutOfRangeException()
        };

        if (result != EditPharmacyResult.Success) return;

        _messenger.Send<PharmaciesChangedNotification>();
        Close();
    }

    
    [RelayCommand]
    private void Add()
    {
        if (IsEditing) return;

        if (WorkingPharmacistId == null)
        {
            ErrorMessage = "Please select the pharmacist that will work in this pharmacy from the list";
            return;
        }

        var result = _adminService!.AddPharmacy(Address, WorkingPharmacistId.Value);

        ErrorMessage = result switch
        {
            AddPharmacyResult.PharmacyAlreadyExists => "Pharmacy at that address already exists",
            AddPharmacyResult.PropsDoNotMeetReqs => "Properties do not meet requirements",
            AddPharmacyResult.Success => "",
            _ => throw new ArgumentOutOfRangeException()
        };

        if (result != AddPharmacyResult.Success) return;
        Close();
    }

    public void Receive(UserExitedMessage message)
    {
        _adminService = null;
    }

    
}