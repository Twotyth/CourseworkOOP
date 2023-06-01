using System.Windows;
using System.Windows.Controls;

namespace PresentationWPF.Views;

public partial class GuestCatalogView : UserControl
{
    public GuestCatalogView()
    {
        InitializeComponent();
    }
    
    private void ShowHint_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox tb) return;
        SearchLabel.Visibility = string.IsNullOrEmpty(tb.Text) 
            ? Visibility.Visible 
            : Visibility.Collapsed;
    }
}