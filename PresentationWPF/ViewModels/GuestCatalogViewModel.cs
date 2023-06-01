using DataLayer.Dtos;
using DataLayer.Repositories;
using Domain.Entities;
using PresentationWPF.Stores;

namespace PresentationWPF.ViewModels;

public class GuestCatalogViewModel : CatalogViewModelBase
{
    
    public GuestCatalogViewModel(
        CartStore cartStore, FilterStore filterStore, IRepos<Product, ProductDto> productRepos, 
        INavigationService loginNavigationService, INavigationService guestCartNavigationService,
        INavigationService filterModalNavigationService
    )
        : base(
            cartStore, filterStore, productRepos, loginNavigationService, 
            guestCartNavigationService, filterModalNavigationService
        )
    {
        
    }
}