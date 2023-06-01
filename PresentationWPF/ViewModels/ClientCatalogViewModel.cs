using System;
using Application.Services.UserServices;
using DataLayer.Dtos;
using DataLayer.Repositories;
using Domain.Entities;
using PresentationWPF.Stores;

namespace PresentationWPF.ViewModels;

public class ClientCatalogViewModel : CatalogViewModelBase
{
    private readonly IClientService _user;

    public ClientCatalogViewModel(
        AccountStore accountStore, CartStore cartStore, FilterStore filterStore, 
        IRepos<Product, ProductDto> productRepos, INavigationService personalCabinetNavigationService, 
        INavigationService clientCartNavigationService, INavigationService filterModalNavigationService
    )
        : base(
            cartStore, filterStore, productRepos, personalCabinetNavigationService, 
            clientCartNavigationService, filterModalNavigationService
        )
    {
        if (accountStore.CurrentUser is not IClientService clientService)
        {
            throw new ArgumentException("Current user was not expected. Expected user: Client");
        }

        _user = clientService;
    }
}