using System;
using System.Collections.Generic;
using Application.Enums;
using Application.InfoObjects;
using Application.Services;
using Application.Services.UserServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Domain.Entities;
using PresentationWPF.Messages;
using PresentationWPF.Stores;
using MedicineInfo = Application.InfoObjects.MedicineInfo;

// ReSharper disable InconsistentNaming

namespace PresentationWPF.ViewModels;

public partial class AdminProductViewModel : PopupViewModelBase, 
    IRecipient<UserExitedMessage>
{
    private IAdminService? _adminService;

    [ObservableProperty]
    private string errorMessage = "";

    private readonly IMessenger _messenger;

    [ObservableProperty]
    private bool isEditing = false;

    public bool IsAdding => !IsEditing;

    public uint? Id { get; set; }
    
    [ObservableProperty] 
    private string name = "";
    
    [ObservableProperty] 
    private string manufacturer = "";
    
    [ObservableProperty] 
    private string description = "";
    
    [ObservableProperty] 
    private string price = "";

    [ObservableProperty] 
    private bool isMedical = false;

    [ObservableProperty] 
    private MedicineInfo? medicine;

    [ObservableProperty] 
    private string dosage = "";
    
    [ObservableProperty] 
    private string quantity = "";

    [ObservableProperty] 
    private DosageForm? dosageForm;

    [ObservableProperty] 
    private QuantityForm? quantityForm;

    [ObservableProperty] 
    private ConsumptionForm? consumptionForm;

    public IEnumerable<DosageForm> DosageForms => Enum.GetValues<DosageForm>();
    public IEnumerable<QuantityForm> QuantityForms => Enum.GetValues<QuantityForm>();
    public IEnumerable<ConsumptionForm> ConsumptionForms => Enum.GetValues<ConsumptionForm>();
    
    public IEnumerable<MedicineInfo> Medicines => ((IRetrieving<MedicineInfo>)_adminService!).Get();


    public AdminProductViewModel(IMessenger messenger, MainViewModel mvm, AccountStore store) : base(mvm)
    {
        _adminService = store.CurrentUser as IAdminService ?? throw new ArgumentException("Current user was not expected. Expected user: Admin");
        
        messenger.RegisterAll(this);
        _messenger = messenger;
    }

    [RelayCommand]
    private void ChangeMedicalOption()
    {
        IsMedical = !IsMedical;
    }
    
    [RelayCommand]
    private void Edit()
    {
        if (!IsEditing) return;
        
        if (Id == null)
        {
            ErrorMessage = "Provided id is not valid";
            return;
        }
        
        if (!decimal.TryParse(Price, out var price))
        {
            ErrorMessage = "Provided price is not valid";
            return;
        }


        ProductMedicineInfo? medicineInfo = null;
        uint dosage, quantity;

        if (IsMedical)
        {
            if (Medicine == null)
            {
                ErrorMessage = "Please select medicine from the list.";
                return;
            }

            if (!uint.TryParse(Dosage, out dosage))
            {
                ErrorMessage = "Provided dosage is not valid";
                return;
            }

            if (!uint.TryParse(Quantity, out quantity))
            {
                ErrorMessage = "Provided quantity is not valid";
                return;
            }

            if (DosageForm == null)
            {
                ErrorMessage = "Please select dosage form from the list";
                return;
            }
            
            if (QuantityForm == null)
            {
                ErrorMessage = "Please select quantity form from the list";
                return;
            }

            if (ConsumptionForm == null)
            {
                ErrorMessage = "Please select consumption form from the list";
                return;
            }

            medicineInfo =
                new ProductMedicineInfo(Medicine.Id, dosage, quantity,  DosageForm.Value, QuantityForm.Value, ConsumptionForm.Value);
        }
        
        var result = _adminService!.EditProduct(Id.Value, Name, Manufacturer, Description, medicineInfo, price);

        ErrorMessage = result switch
        {
            EditProductResult.ProductNotFound => "Product was not found",
            EditProductResult.NewValueDoesNotMeetReqs => "New values do not meet requirements",
            EditProductResult.NewMedicineNotFound => "New medicine was not found",
            EditProductResult.Success => "",
            _ => throw new ArgumentOutOfRangeException()
        };

        if (result != EditProductResult.Success) return;

        _messenger.Send<ProductsChangedNotification>();
        Close();
    }

    [RelayCommand]
    private void Add()
    {
        if (IsEditing) return;
        
        if (!decimal.TryParse(Price, out var price))
        {
            ErrorMessage = "Provided price is not valid";
            return;
        }


        ProductMedicineInfo? medicineInfo = null;
        uint dosage, quantity;

        if (IsMedical)
        {
            if (Medicine == null)
            {
                ErrorMessage = "Please select medicine from the list.";
                return;
            }

            if (!uint.TryParse(Dosage, out dosage))
            {
                ErrorMessage = "Provided dosage is not valid";
                return;
            }

            if (!uint.TryParse(Quantity, out quantity))
            {
                ErrorMessage = "Provided quantity is not valid";
                return;
            }

            if (DosageForm == null)
            {
                ErrorMessage = "Please select dosage form from the list";
                return;
            }
            
            if (QuantityForm == null)
            {
                ErrorMessage = "Please select quantity form from the list";
                return;
            }

            if (ConsumptionForm == null)
            {
                ErrorMessage = "Please select consumption form from the list";
                return;
            }

            medicineInfo =
                new ProductMedicineInfo(Medicine.Id, dosage, quantity, DosageForm.Value, QuantityForm.Value, ConsumptionForm.Value);
        }

        var result = _adminService!.AddProduct(Name, Manufacturer, Description, price, medicineInfo);

        ErrorMessage = result switch
        {
            AddProductResult.ProductAlreadyExists => "Product with this name already exists",
            AddProductResult.PropsDoNotMeetReqs => "Properties of the product do not meet requirements",
            AddProductResult.Success => "",
            _ => throw new ArgumentOutOfRangeException()
        };

        if (result != AddProductResult.Success) return;
        _messenger.Send<ProductsChangedNotification>();
        Close();
    }

    public void Receive(UserExitedMessage message)
    {
        _adminService = null;
    }
}