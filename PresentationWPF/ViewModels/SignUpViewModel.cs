using System;
using System.Diagnostics.CodeAnalysis;
using Application.Services.UserServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PresentationWPF.Stores;

namespace PresentationWPF.ViewModels;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public partial class SignUpViewModel : ModalViewModelBase
{
    private readonly AccountStore _accountStore;
    private readonly IGuestService _guestService;
    private readonly INavigationService _clientCatalogNavigationService;

    public string Password { get; set; } = "";
    public string PasswordRepeat { get; set; } = "";
    public string Username { get; set; } = "";
    
    [ObservableProperty]
    private string errorMessage = ""; 
    
    public SignUpViewModel(
        MainViewModel mvm, AccountStore accountStore, IGuestService guestService, 
        INavigationService clientCatalogNavigationService
        ) : base(mvm)
    {
        _accountStore = accountStore;
        _guestService = guestService;
        _clientCatalogNavigationService = clientCatalogNavigationService;
    }

    [RelayCommand]
    private void SignUp()
    {
        try
        {
            if (PasswordRepeat != Password) throw new Exception("Passwords do not match. Fill them carefully");
            _accountStore.CurrentUser = _guestService.Register(Username, Password);
            _clientCatalogNavigationService.Navigate();
            Close();
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }
}