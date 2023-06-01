using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using PresentationWPF.ViewModels;

namespace PresentationWPF.Stores;

public interface INavigationService
{
    public void Navigate();
}

public interface INavigatable
{
    public ObservableObject? CurrentViewModel { get; set; }
    public ObservableObject CurrentModalViewModel { get; set; }
    public ObservableObject CurrentPopupViewModel { get; set; }
}

public interface IParamNavigationService<TParam> : INavigationService
{
    public TParam? Param { get; set; }
}

public class NavigationService : INavigationService
{
    private readonly INavigatable _mvm;
    private readonly Func<ObservableObject> _getter;

    public NavigationService(INavigatable mvm, Func<ObservableObject> getter)
    {
        _mvm = mvm;
        _getter = getter;
    }

    public void Navigate()
    {
        _mvm.CurrentViewModel = _getter.Invoke();
    }

    public static NavigationService Get<T>(IServiceProvider s) where T : ObservableObject =>
        new(s.GetRequiredService<MainViewModel>(), s.GetRequiredService<T>);
}

public class PopupNavigationService : INavigationService
{
    private readonly INavigatable _mvm;
    private readonly Func<ObservableObject> _getter;

    public PopupNavigationService(INavigatable mvm, Func<PopupViewModelBase> getter)
    {
        _mvm = mvm;
        _getter = getter;
    }

    public void Navigate()
    {
        _mvm.CurrentPopupViewModel = _getter.Invoke();

        if (_mvm is not MainViewModel mvm) return;
        mvm.IsHitTestVisible = false;
        mvm.IsPopupOpen = true;
        mvm.BlurRadius = 5;
    }

    public static PopupNavigationService Get<T>(IServiceProvider s) where T : PopupViewModelBase =>
        new(s.GetRequiredService<MainViewModel>(), s.GetRequiredService<T>);
}

public class ModalNavigationService : INavigationService
{
    private readonly INavigatable _mvm;
    private readonly Func<ObservableObject> _getter;

    public ModalNavigationService(INavigatable mvm, Func<ModalViewModelBase> getter)
    {
        _mvm = mvm;
        _getter = getter;
    }

    public void Navigate()
    {
        _mvm.CurrentModalViewModel = _getter.Invoke();
        
        if (_mvm is not MainViewModel mvm) return;
        mvm.IsHitTestVisible = false;
        mvm.IsModalOpen = true;
    }

    public static ModalNavigationService Get<T>(IServiceProvider s) where T : ModalViewModelBase =>
        new(s.GetRequiredService<MainViewModel>(), s.GetRequiredService<T>);
}