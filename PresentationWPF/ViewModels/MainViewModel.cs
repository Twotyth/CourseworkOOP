using System.Net.Mime;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PresentationWPF.Stores;
// ReSharper disable InconsistentNaming

namespace PresentationWPF.ViewModels;

public partial class MainViewModel : ObservableObject, INavigatable
{
    // private readonly NavigationStore _navigationStore;
    // public ObservableObject CurrentViewModel => _navigationStore.CurrentViewModel;

    [ObservableProperty]
    private ObservableObject currentViewModel;

    [ObservableProperty]
    private ObservableObject? _currentPopupViewModel;

    [ObservableProperty]
    private ObservableObject? _currentModalViewModel;

    [ObservableProperty]
    private bool _isPopupOpen = false;

    [ObservableProperty]
    private bool _isModalOpen = false;

    [ObservableProperty]
    private bool _isHitTestVisible = true;

    [ObservableProperty]
    private uint blurRadius = 0;
    
    public MainViewModel()
    {
        // _navigationStore = navigationStore;
    }

    [RelayCommand]
    public void Close()
    {
        System.Windows.Application.Current.Shutdown();
    }
}