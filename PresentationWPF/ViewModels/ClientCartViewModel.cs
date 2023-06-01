using System;
using System.Linq;
using System.Windows.Media;
using Application.InfoObjects;
using Application.Services;
using Application.Services.UserServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PresentationWPF.Messages;
using PresentationWPF.Stores;
// ReSharper disable InconsistentNaming

namespace PresentationWPF.ViewModels;

public sealed partial class ClientCartViewModel : CartViewModelBase
{
    private readonly IClientService _client;

    public ClientCartViewModel(
        IMessenger messenger, AccountStore store,
        INavigationService catalogNavigationService,
        INavigationService profileNavigationService
        ) : base(messenger, catalogNavigationService, profileNavigationService)
    {
        if (store.CurrentUser is not IClientService clientService)
        {
            throw new ArgumentException("Current user was not expected. Expected user: Client");
        }

        _client = clientService;

        PharmacyAddresses = ((IRetrieving<(string, uint)>)_client).Get().Select(i => new PharmacyInfo(i.Item2, i.Item1, null)).ToArray();
        CurrentBalance = _client.GetBalance();
    }

    [ObservableProperty]
    private decimal bonusMoney;

    [ObservableProperty]
    private bool isUsingMoney = false;
    public decimal CurrentBalance { get; }

    public override void Receive(CartItemQuantityChanged message)
    {
        base.Receive(message);
        BonusMoney = Math.Round(TotalPrice / 100, 2, MidpointRounding.ToZero);
    }

    protected override void Receive(CartItemsRequestMessage message)
    {
        base.Receive(message);
        BonusMoney = Math.Round(TotalPrice / 100, 2, MidpointRounding.ToZero);
    }

    public override void Receive(CartItemRemovedMessage message)
    {
        base.Receive(message);
        BonusMoney = Math.Round(TotalPrice / 100, 2, MidpointRounding.ToZero);
    }

    public override void Receive(CartItemAddedMessage message)
    {
        base.Receive(message);
        BonusMoney = Math.Round(TotalPrice / 100, 2, MidpointRounding.ToZero);
    }

    [RelayCommand]
    private void ChangeUsingMoneyOption()
    {
        IsUsingMoney = !IsUsingMoney;
    }
    
    protected override void Checkout()
    {
        if (!CartItems.Any())
        {
            MessageBrush = new SolidColorBrush(Colors.Red);
            Message = "Cannot order empty list of products.";
        }
        
        if (SelectedDeliveryPharmacy == null)
        {
            MessageBrush = new SolidColorBrush(Colors.Red);
            Message = "Please select delivery address.";
            return;
        }

        try
        {
            uint? prescription;
            decimal withdrawMoney = 0;

            if (PrescriptionId == "")
                prescription = null;
            else
            {
                try
                {
                    prescription = uint.Parse(PrescriptionId);
                }
                catch
                {
                    throw new Exception("Provided prescription ID is not valid.");
                }
            }


            if (IsUsingMoney)
            {
                withdrawMoney = CurrentBalance;
            }

            _client.Order(
                CartItems.ToDictionary(i => i.Id, i => i.Quantity),
                SelectedDeliveryPharmacy.Id, prescription, withdrawMoney
            );

            while (CartItems.Any())
            {
                CartItems[0].RemoveFromCart();
            }

            MessageBrush = new SolidColorBrush(Colors.Green);
            Message = "Successfully ordered! You can see the order and its status in your personal cabinet";
        }
        catch (Exception e)
        {
            MessageBrush = new SolidColorBrush(Colors.Red);
            Message = e.Message;
        }
    }
}