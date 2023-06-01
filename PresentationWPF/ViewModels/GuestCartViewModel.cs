using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using Application.InfoObjects;
using Application.Services;
using Application.Services.UserServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PresentationWPF.Stores;

// ReSharper disable InconsistentNaming

namespace PresentationWPF.ViewModels;

public sealed partial class GuestCartViewModel : CartViewModelBase
{
    private readonly IGuestService _guestService;
    private readonly INavigationService _signUpNavigationService;

    public GuestCartViewModel(
        IMessenger messenger, IGuestService guestService, 
        INavigationService backNavigationService,
        INavigationService loginNavigationService,
        INavigationService signUpNavigationService
        ) : base(messenger, backNavigationService, loginNavigationService)
    {
        _guestService = guestService;
        _signUpNavigationService = signUpNavigationService;

        PharmacyAddresses = ((IRetrieving<(string, uint)>)_guestService).Get().Select(i => new PharmacyInfo(i.Item2, i.Item1, null)).ToArray();
    }

    [RelayCommand]
    private void SignUpNavigate()
    {
        _signUpNavigationService.Navigate();
    }
    
    protected override void Checkout()
    {
        if (!CartItems.Any())
        {
            MessageBrush = new SolidColorBrush(Colors.Red);
            Message = "Cannot order empty list of products.";
            return;
        }
        
        if (SelectedDeliveryPharmacy == null)
        {
            MessageBrush = new SolidColorBrush(Colors.Red);
            Message = "Please select delivery address.";
            return;
        }

        uint? prescription = PrescriptionId == "" ? null : uint.Parse(PrescriptionId);
        try
        {
            var order = _guestService.Order(
                CartItems.ToDictionary(i => i.Id, i => i.Quantity),
                SelectedDeliveryPharmacy.Id, prescription
            );

            while (CartItems.Any())
            {
                CartItems[0].RemoveFromCart();
            }
            
            MessageBrush = new SolidColorBrush(Colors.Green);
            Message = $"Successfully ordered! Your order ID is: {order.Id}. Please memorize it -- you'll have to provide it when picking order up.";
        }
        catch (Exception e)
        {
            MessageBrush = new SolidColorBrush(Colors.Red);
            Message = e.Message;
        }
    }
}