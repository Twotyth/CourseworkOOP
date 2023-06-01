using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Application.InfoObjects;
using Application.Services;
using Application.Services.UserServices;
using CommunityToolkit.Mvvm.Messaging;
using Domain.Common;
using PresentationWPF.Stores;
// ReSharper disable InconsistentNaming

namespace PresentationWPF.ViewModels;

public partial class ClientCabinetViewModel : PersonalCabinetViewModel
{
    public ClientCabinetViewModel(
        IMessenger messenger,
        AccountStore accountStore, MainViewModel mvm, 
        INavigationService guestCatalogNavigationService
        ) : base(messenger, accountStore, mvm, guestCatalogNavigationService)
    {
        if (accountStore.CurrentUser is not IClientService clientService)
        {
            throw new ArgumentException("Current user was not expected. Expected user: Client");
        }

        Balance = clientService.GetBalance().ToString("N2");

        var userOrders = ((IRetrieving<OrderInfo>)clientService).Get().ToArray();
        var pharmacies = ((IRetrieving<(string, uint)>)clientService).Get();
        
        OrdersToDeliver = new(
            userOrders
                .Where(i => i.Status == OrderStatus.Delivering)
                .Select(i => new OrderViewData(
                    i.ProductNamesQuantity, pharmacies.First(p => p.Item2 == i.DeliveryPharmacyId).Item1, i.OrderDate)
                )
                .OrderBy(i => i.OrderDate)
        );
        
        AllOrders = new(
            userOrders
                .Where(i => i.Status != OrderStatus.Delivering)
                .Select(i => new OrderViewData(
                    i.ProductNamesQuantity, pharmacies.First(p => p.Item2 == i.DeliveryPharmacyId).Item1, i.OrderDate)
                )
                .OrderBy(i => i.OrderDate)
        );
    }

    
    public string Balance { get; }
    public ObservableCollection<OrderViewData> OrdersToDeliver { get; }
    public ObservableCollection<OrderViewData> AllOrders { get; }
}

public record OrderViewData(IDictionary<string, uint> ProductNameQuantity, string DeliveryPharmacy, DateOnly OrderDate);