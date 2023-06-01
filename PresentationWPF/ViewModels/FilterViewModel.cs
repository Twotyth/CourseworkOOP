using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DataLayer.Dtos;
using DataLayer.Repositories;
using Domain.Entities;
using PresentationWPF.Messages;
using PresentationWPF.Stores;
// ReSharper disable InconsistentNaming

namespace PresentationWPF.ViewModels;

public partial class FilterItemViewModel : ObservableObject
{
    private readonly ICollection<FilterItemViewModel> _allItems;
    private readonly ICollection<SelectedFilterItemViewModel> _selectedItems;
    public string Value { get; }

    public FilterItemViewModel(
        string value, ICollection<FilterItemViewModel> allItems, 
        ICollection<SelectedFilterItemViewModel> selectedItems
        )
    {
        _allItems = allItems;
        _selectedItems = selectedItems;
        Value = value;
    }

    [RelayCommand]
    public void AddToFilter()
    {
        _selectedItems.Add(new SelectedFilterItemViewModel(Value, _allItems, _selectedItems));
        
        _allItems.Remove(this);
    }
}

public partial class SelectedFilterItemViewModel : ObservableObject
{
    private readonly ICollection<FilterItemViewModel> _allItems;
    private readonly ICollection<SelectedFilterItemViewModel> _selectedItems;
    public string Value { get; }

    public SelectedFilterItemViewModel(
        string value, ICollection<FilterItemViewModel> allItems,
        ICollection<SelectedFilterItemViewModel> selectedItems
    )
    {
        _allItems = allItems;
        _selectedItems = selectedItems;
        Value = value;
    }

    [RelayCommand]
    protected void RemoveFromFilter()
    {
        _allItems.Add(new FilterItemViewModel(Value, _allItems, _selectedItems));
        _selectedItems.Remove(this);
    }
}



public sealed partial class FilterViewModel : PopupViewModelBase
{
    private readonly IMessenger _messenger;
    private readonly FilterStore _store;
    public ObservableCollection<FilterItemViewModel> AllManufacturers { get; } = new();
    public ObservableCollection<SelectedFilterItemViewModel> SelectedManufacturers { get; } = new();
    public ObservableCollection<FilterItemViewModel> AllTypes { get; } = new();
    public ObservableCollection<SelectedFilterItemViewModel> SelectedTypes { get; } = new();
    public ObservableCollection<FilterItemViewModel> AllCategories { get; } = new();
    public ObservableCollection<SelectedFilterItemViewModel> SelectedCategories { get; } = new();
    

    [ObservableProperty]
    private Visibility manufacturerListVisibility = Visibility.Collapsed;

    [ObservableProperty]
    private Visibility typeListVisibility = Visibility.Collapsed;

    [ObservableProperty]
    private Visibility categoryListVisibility = Visibility.Collapsed;

    public decimal PriceRangeMin { get; }

    public decimal PriceRangeMax { get; }

    public FilterViewModel(
        IMessenger messenger, MainViewModel mvm, 
        FilterStore store,
        IRepos<Product, ProductDto> productRepos
        ) : base(mvm)
    {
        _messenger = messenger;
        _store = store;

        PriceRangeMin = productRepos.Min(i => i.Price);
        PriceRangeMax = productRepos.Max(i => i.Price);
        
        var allManufacturers = productRepos.GroupBy(i => i.Manufacturer)
            .Select(p => new FilterItemViewModel(p.Key, AllManufacturers, SelectedManufacturers))
            .Distinct();
        foreach (var item in allManufacturers)
        {
            AllManufacturers.Add(item);
        }
        
        var allTypes =productRepos.Where(p => p.MedicineInfo != null).GroupBy(i => i.MedicineInfo!.Medicine.Type)
            .Select(p => new FilterItemViewModel(p.Key, AllTypes, SelectedTypes))
            .Distinct();
        foreach (var item in allTypes)
        {
            AllTypes.Add(item);
        }
        
        var allCategories = productRepos
            .Where(p => p.MedicineInfo != null).GroupBy(i => i.MedicineInfo!.Medicine.TypeCategory)
            .Select(p => new FilterItemViewModel(p.Key, AllCategories, SelectedCategories))
            .Distinct();
        foreach (var item in allCategories)
        {
            AllCategories.Add(item);
        }

        var responseManufacturers = messenger.Send<ManufacturerSelectionRequestMessage>();
        Recieve(responseManufacturers);

        var responseType = messenger.Send<TypeSelectionRequestMessage>();
        Recieve(responseType);

        var responseCategories = messenger.Send<CategorySelectionRequestMessage>();
        Recieve(responseCategories);
        
        
    }

    public override void Close()
    {
        _store.Clear();
        
        foreach (var category in SelectedCategories)
        {
            _store.AddCategory(category.Value);
        }

        foreach (var manufacturer in SelectedManufacturers)
        {
            _store.AddManufacturer(manufacturer.Value);
        }

        foreach (var type in SelectedTypes)
        {
            _store.AddType(type.Value);
        }
        
        base.Close();
    }

    private void Recieve(CategorySelectionRequestMessage responseCategories)
    {
        foreach (var category in responseCategories.Response)
        {
            var typeFilterItem = AllTypes.FirstOrDefault(i => i!.Value == category, null);
            typeFilterItem?.AddToFilter();
        }
    }

    private void Recieve(TypeSelectionRequestMessage responseTypes)
    {
        foreach (var type in responseTypes.Response)
        {
            var typeFilterItem = AllTypes.FirstOrDefault(i => i!.Value == type, null);
            typeFilterItem?.AddToFilter();
        }
    }

    private void Recieve(ManufacturerSelectionRequestMessage responseManufacturers)
    {
        foreach (var manufacturer in responseManufacturers.Response)
        {
            var typeFilterItem = AllManufacturers.FirstOrDefault(i => i!.Value == manufacturer, null);
            typeFilterItem?.AddToFilter();
        }
    }


    [RelayCommand]
    private void ResetFilters()
    {
        SelectedCategories.Clear();
        SelectedManufacturers.Clear();
        SelectedTypes.Clear();
    }

    [RelayCommand]
    private void ChangeManufacturerListVisibility()
    {
        ManufacturerListVisibility = ManufacturerListVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
    }
    
    [RelayCommand]
    private void ChangeTypeListVisibility()
    {
        TypeListVisibility = TypeListVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
    }
    
    [RelayCommand]
    private void ChangeCategoryListVisibility()
    {
        CategoryListVisibility = CategoryListVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
    }
}