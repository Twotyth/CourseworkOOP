using System;
using Application.Services.UserServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PresentationWPF.Stores;
// ReSharper disable InconsistentNaming

namespace PresentationWPF.ViewModels;

public sealed partial class LoginViewModel : ModalViewModelBase
{
    private readonly AccountStore _accountStore;
    private readonly IGuestService _guestService;
    private readonly INavigationService _signUpNavigationService;
    private readonly INavigationService _afterLoginNavigationService;


    public string Password { get; set; } = "";
    public string Username { get; set; } = "";

    [ObservableProperty]
    private string errorMessage = "";
    
    

    public LoginViewModel(
        MainViewModel mvm,AccountStore accountStore, IGuestService guestService, 
        INavigationService signUpNavigationService,
        INavigationService afterLoginNavigationService
        ) : base(mvm)
    {
        _accountStore = accountStore;
        _guestService = guestService;
        _signUpNavigationService = signUpNavigationService;
        _afterLoginNavigationService = afterLoginNavigationService;
    }

    [RelayCommand]
    private void Login()
    {
        try
        {
            _accountStore.CurrentUser = _guestService.Login(Username, Password);
            _afterLoginNavigationService.Navigate();
            Close();
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }

    [RelayCommand]
    private void SignUpNavigation()
    {
        _signUpNavigationService.Navigate();
        
        if (_signUpNavigationService is not ModalNavigationService) Close();
    }
}