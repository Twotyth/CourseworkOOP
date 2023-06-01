using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PresentationWPF.Stores;

namespace PresentationWPF.ViewModels;

public abstract partial class PopupViewModelBase : ObservableObject
{
    private readonly MainViewModel _mvm;

    protected PopupViewModelBase(MainViewModel mvm)
    {
        _mvm = mvm;
    }

    [RelayCommand]
    public virtual void Close()
    {
        _mvm.IsPopupOpen = false;
        _mvm.IsHitTestVisible = true;
        _mvm.BlurRadius = 0;
    }
}

public abstract partial class ModalViewModelBase : ObservableObject
{
    private readonly MainViewModel _mvm;

    protected ModalViewModelBase(MainViewModel mvm)
    {
        _mvm = mvm;
    }

    [RelayCommand]
    public void Close()
    {
        _mvm.IsModalOpen = false;
        _mvm.IsHitTestVisible = true;
    }
}