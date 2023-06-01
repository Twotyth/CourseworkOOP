using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Application.InfoObjects;
using CommunityToolkit.Mvvm.Messaging;
using DataLayer.Dtos;
using DataLayer.Repositories;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using PresentationWPF.Messages;
using PresentationWPF.ViewModels;
using MedicineInfo = Application.InfoObjects.MedicineInfo;

namespace PresentationWPF.Stores;

public sealed class FilterStore : 
    IRecipient<FiltersResetMessage>, IRecipient<FilterSelectionChangedMessage>,
    IRecipient<UserExitedMessage>
{
    private readonly IEnumerable<Product> _productsRepos;
    
    public FilterStore(IMessenger messenger, IRepos<Product, ProductDto> productsRepos)
    {
        _productsRepos = productsRepos.Where(i => i.IsOnSale);

        // PriceRange = (productsRepos.Min(i => i.Price), productsRepos.Max(i=>i.Price));
        
        messenger.RegisterAll(this);
        
        messenger.Register<FilterStore, ManufacturerSelectionRequestMessage>(
            this, 
            (recipient, message) => message.Reply(recipient._manufacturers)
        );
        messenger.Register<FilterStore, TypeSelectionRequestMessage>(
            this, 
            (recipient, message) => message.Reply(recipient._types)
        );
        messenger.Register<FilterStore, CategorySelectionRequestMessage>(
            this, 
            (recipient, message) => message.Reply(recipient._categories)
        );
        messenger.Register<FilterStore, RangeSelectionRequestMessage>(
            this, 
            (recipient, message) => message.Reply(recipient.PriceRange)
        );
    }

    private (decimal Start, decimal End) PriceRange { get; set; }
    private readonly List<string> _manufacturers = new();
    private readonly List<uint> _quantity = new();
    private readonly List<string> _types = new();
    private readonly List<string> _categories = new();

    public IEnumerable<ProductInfo> ApplyFilter(string searchString)
    {
        List<Product> products = new();
        products = searchString.Trim() switch
        {
            "" => new(_productsRepos),
            _ => new(_productsRepos.Where(
                p => p.Name.Contains(searchString) || p.Manufacturer.Contains(searchString) || 
                     p.MedicineInfo == null || p.MedicineInfo.Medicine.Name.Contains(searchString)
            ))
        };

        return products.Where(Check).Select(
            p => new ProductInfo(
                p.Id, p.FullName, p.Manufacturer, p.Description, p.IsOnSale,
                p.MedicineInfo == null
                    ? null
                    : new ProductMedicineInfo(p.MedicineInfo.Medicine.Id, p.MedicineInfo.Dosage,
                        p.MedicineInfo.Quantity,
                        p.MedicineInfo.DosageForm, p.MedicineInfo.QuantityForm,
                        p.MedicineInfo.ConsumptionForm),
                p.Price
            )
        );
    }

    private bool Check(Product product)
    {
        if (!product.IsOnSale)
        {
            return false;
        }
        
        if (product.Price < PriceRange.Start || product.Price > PriceRange.End)
        {
            return false;
        }

        if (_manufacturers.Any() && !_manufacturers.Contains(product.Manufacturer))
        {
            return false;
        }

        if (product.MedicineInfo == null)
        {
            if (_types.Any()) return false;
            if (_categories.Any()) return false;
            if (_quantity.Any()) return false;
        }
        else 
        {
            if (_types.Any() && !_types.Contains(product.MedicineInfo.Medicine.Type))
            {
                return false;
            }

            if (_categories.Any() && !_categories.Contains(product.MedicineInfo.Medicine.TypeCategory))
            {
                return false;
            }

            if (_quantity.Any() && !_quantity.Contains(product.MedicineInfo.Quantity))
            {
                return false;
            }
        }

        return true;
    }
    
    public void AddManufacturer(string value)
    {
        _manufacturers.Add(value);
    }

    public void RemoveManufacturer(string value)
    {
        _manufacturers.Remove(value);
    }

    public void AddQuantity(uint value)
    {
        _quantity.Add(value);
    }

    public void RemoveQuantity(uint value)
    {
        _quantity.Remove(value);
    }

    public void AddType(string value)
    {
        _types.Add(value);
    }

    public void RemoveType(string value)
    {
        _types.Remove(value);
    }

    public void AddCategory(string value)
    {
        _categories.Add(value);
    }

    public void RemoveCategory(string value)
    {
        _categories.Remove(value);
    }

    public void Clear()
    {
        _manufacturers.Clear();
        _types.Clear();
        _categories.Clear();
        _quantity.Clear();
        PriceRange = (_productsRepos.Min(i => i.Price), _productsRepos.Max(i => i.Price));
    }    
    

    public void Receive(FiltersResetMessage message)
    {
        PriceRange = (_productsRepos.Min(i=>i.Price), _productsRepos.Max(i=>i.Price));
        _manufacturers.Clear();
        _quantity.Clear();
        _types.Clear();
        _categories.Clear();
    }

    public void Receive(FilterSelectionChangedMessage message)
    {
        IList list = message.FilterList switch
        {
            FilterList.Manufacturers => _manufacturers,
            FilterList.Types => _types,
            FilterList.Categories => _categories,
            FilterList.Quantity => _quantity,
            _ => throw new ArgumentOutOfRangeException(nameof(message.FilterList))
        };

        list.Add(message.Value);
    }

    public void Receive(UserExitedMessage message)
    {
        _manufacturers.Clear();
        _quantity.Clear();
        _categories.Clear();
        _types.Clear();
    }
}