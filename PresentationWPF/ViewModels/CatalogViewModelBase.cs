using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataLayer.Dtos;
using DataLayer.Repositories;
using Domain.Entities;
using PresentationWPF.EntityItems.CartItem;
using PresentationWPF.Stores;
// ReSharper disable InconsistentNaming

namespace PresentationWPF.ViewModels;

public abstract partial class CatalogViewModelBase : ObservableObject
{
    private readonly CartStore _store;
    private readonly FilterStore _filterStore;

    [ObservableProperty]
    private string titleSearched = "";

    private readonly INavigationService _loginOrPersonalCabinetNavigationService;
    private readonly INavigationService _cartNavigationService;
    private readonly INavigationService _filterModalNavigationService;
    public ObservableCollection<CatalogItemViewModel> ViewingProducts { get; private set; }

    protected CatalogViewModelBase(
        CartStore store, FilterStore filterStore, IRepos<Product, ProductDto> productRepos, 
        INavigationService loginOrPersonalCabinetNavigationService, INavigationService cartNavigationService,
        INavigationService filterModalNavigationService
    )
    {
        _store = store;
        _filterStore = filterStore;
        _loginOrPersonalCabinetNavigationService = loginOrPersonalCabinetNavigationService;
        _cartNavigationService = cartNavigationService;
        _filterModalNavigationService = filterModalNavigationService;
        
        ViewingProducts = new ObservableCollection<CatalogItemViewModel>(
            productRepos.Where(i => i.IsOnSale).Select(
                p => new CatalogItemViewModel(p.Id, p.FullName, p.Manufacturer, p.Description, p.Price, 
                    p.MedicineInfo == null 
                        ? null 
                        : new CatalogItemViewModelMedicineInfo(
                            p.MedicineInfo.Medicine.Id, p.MedicineInfo.Dosage, p.MedicineInfo.Quantity, p.MedicineInfo.DosageForm, p.MedicineInfo.QuantityForm, p.MedicineInfo.ConsumptionForm
                        ), 
                    _store
                )
            )
        );
    }

    [RelayCommand]
    private void LoginOrPersonalCabinet()
    {
        _loginOrPersonalCabinetNavigationService.Navigate();
    }

    [RelayCommand]
    private void CartNavigate()
    {
        _cartNavigationService.Navigate();
    }

    [RelayCommand]
    private void Search()
    {
        var filteredItems = _filterStore.ApplyFilter(TitleSearched).Select(x => 
            new CatalogItemViewModel(
                x.Id, x.Name, x.Manufacturer, x.Description, x.Price, x.MedicineInfo == null 
                    ? null 
                    : new(
                        x.MedicineInfo.MedicineId, x.MedicineInfo.Dosage, x.MedicineInfo.Quantity, 
                        x.MedicineInfo.DosageForm, x.MedicineInfo.QuantityForm, x.MedicineInfo.ConsumptionForm
                    ), 
                _store
            )
        );
        ViewingProducts.Clear();

        foreach (var item in filteredItems)
        {
            ViewingProducts.Add(item);
        }
        
    }

    [RelayCommand]
    private void FilterModalNavigate()
    {
        _filterModalNavigationService.Navigate();
    }
}

public partial class CatalogItemViewModel : ObservableObject
{
    private readonly CartStore _store;
    public uint Id { get; }
    public string Name { get; }
    public string Manufacturer { get; }
    public decimal Price { get; }
    public CatalogItemViewModelMedicineInfo? MedicineInfo { get; }
    public string Description { get; }

    public CatalogItemViewModel(uint id, string name, string manufacturer, string description, decimal price, CatalogItemViewModelMedicineInfo? medicineInfo, CartStore store)
    {
        _store = store;
        Id = id;
        Name = name;
        Manufacturer = manufacturer;
        Description = description;
        Price = price;
        MedicineInfo = medicineInfo;
    }

    [RelayCommand]
    private void AddToCart()
    {
        _store.AddToCart(new CartItem(Id, 1, Name, Manufacturer, Price));
    }
}

public sealed class CatalogItemViewModelMedicineInfo
{
    public CatalogItemViewModelMedicineInfo(uint medicine, uint dosage, uint quantity, DosageForm dosageForm, QuantityForm quantityForm, ConsumptionForm consumptionForm)
    {
        MedicineId = medicine;
        Dosage = dosage;
        Quantity = quantity;
        DosageForm = dosageForm;
        QuantityForm = quantityForm;
        ConsumptionForm = consumptionForm;
    }
    public uint MedicineId { get; set; }
    public uint Dosage { get; set; }
    public uint Quantity { get; set; }
    public DosageForm DosageForm { get; }
    public QuantityForm QuantityForm { get; }
    public ConsumptionForm ConsumptionForm { get; }
}

