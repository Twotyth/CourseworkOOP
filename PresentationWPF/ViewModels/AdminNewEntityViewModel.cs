using System;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PresentationWPF.Messages;

namespace PresentationWPF.ViewModels;

public partial class AdminNewEntityViewModel : PopupViewModelBase,
    IRecipient<EditProductMessage>, IRecipient<EditPharmacyMessage>, IRecipient<EditPharmacistMessage>
{
    public Func<AdminProductViewModel> ProductViewModel { get; }
    public Func<AdminPharmacistViewModel> PharmacistViewModel { get; }
    public Func<AdminPharmacyViewModel> PharmacyViewModel { get; }

    [ObservableProperty]
    public ObservableObject? currentViewModel;

    [ObservableProperty]
    private bool isAdding = true; 

    public AdminNewEntityViewModel(
        IMessenger messenger,
        MainViewModel mvm,
        Func<AdminProductViewModel> adminProductViewModel, 
        Func<AdminPharmacistViewModel> adminPharmacistViewModel, 
        Func<AdminPharmacyViewModel> adminPharmacyViewModel
        ) : base(mvm)
    {
        messenger.RegisterAll(this);
        ProductViewModel = adminProductViewModel;
        PharmacistViewModel = adminPharmacistViewModel;
        PharmacyViewModel = adminPharmacyViewModel;
    }

    [RelayCommand]
    private void ProductNavigate()
    {
        CurrentViewModel = ProductViewModel.Invoke();
    }

    [RelayCommand]
    private void PharmacyNavigate()
    {
        CurrentViewModel = PharmacyViewModel.Invoke();
    }

    [RelayCommand]
    private void PharmacistNavigate()
    {
        CurrentViewModel = PharmacistViewModel.Invoke();
    }
    
    public void Receive(EditProductMessage message)
    {
        IsAdding = false;

        var product = ProductViewModel.Invoke();

        product.IsEditing = true;
        
        product.Id = message.ProductInfo.Id;
        product.Name = message.ProductInfo.Name;
        product.Manufacturer = message.ProductInfo.Manufacturer;
        product.Description = message.ProductInfo.Description;
        product.Price = message.ProductInfo.Price.ToString("C0");
                
        if (message.ProductInfo.MedicineInfo == null)
        {
            CurrentViewModel = product;
            return;
        }

        product.IsMedical = true;
        
        product.Medicine = product.Medicines.First(i => i.Id == message.ProductInfo.MedicineInfo.MedicineId);
        
        product.Dosage = message.ProductInfo.MedicineInfo.Dosage.ToString();
        product.Quantity = message.ProductInfo.MedicineInfo.Quantity.ToString();
        product.DosageForm = message.ProductInfo.MedicineInfo.DosageForm;
        product.QuantityForm = message.ProductInfo.MedicineInfo.QuantityForm;
        product.ConsumptionForm = message.ProductInfo.MedicineInfo.ConsumptionForm;

        CurrentViewModel = product;
    }
    
    public void Receive(EditPharmacyMessage message)
    {
        IsAdding = false;

        var pharmacy = PharmacyViewModel.Invoke();

        pharmacy.IsEditing = true;
        
        pharmacy.Id = message.PharmacyInfo.Id;
        pharmacy.Address = message.PharmacyInfo.Address;

        CurrentViewModel = pharmacy;
    }
    
    
    public void Receive(EditPharmacistMessage message)
    {
        IsAdding = false;

        var pharmacist = PharmacistViewModel.Invoke();

        pharmacist.IsEditing = true;
        
        pharmacist.Id = message.PharmacistInfo.Id;
        pharmacist.Salary = message.PharmacistInfo.Salary.ToString();

        CurrentViewModel = pharmacist;
    }
}