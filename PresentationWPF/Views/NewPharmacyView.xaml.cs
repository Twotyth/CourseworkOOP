using System.Windows;
using System.Windows.Controls;

namespace PresentationWPF.Views;

public partial class NewPharmacyView : UserControl
{
    public NewPharmacyView()
    {
        InitializeComponent();
    }

    private void ShowAddressHint_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox tb) return;

        AddressHint.Visibility = tb.Text == "" ? Visibility.Visible : Visibility.Collapsed;
        
    }

    private void ShowPharmacistHint_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox cb) return;
        
        PharmacistHint.Visibility = cb.SelectedIndex != -1 ? Visibility.Collapsed : Visibility.Visible;
    }
}