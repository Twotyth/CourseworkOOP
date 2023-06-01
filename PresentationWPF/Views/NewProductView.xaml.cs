using System.Windows;
using System.Windows.Controls;

namespace PresentationWPF.Views;

public partial class NewProductView : UserControl
{
    public NewProductView()
    {
        InitializeComponent();
    }

    private void NameBoxShowNameHint_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox tb) return;

        NameHint.Visibility = tb.Text == "" ? Visibility.Visible : Visibility.Collapsed;
    }

    private void NameBoxShowManufacturerHint_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox tb) return;

        ManufacturerHint.Visibility = tb.Text == "" ? Visibility.Visible : Visibility.Collapsed;
    }

    private void MedicineBoxShowHint_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox cb) return;

        MedicineHint.Visibility = cb.SelectedIndex != -1 ? Visibility.Collapsed : Visibility.Visible;
    }

    private void DosageBoxShowHint_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox cb) return;

        DosageFormHint.Visibility = cb.SelectedIndex != -1 ? Visibility.Collapsed : Visibility.Visible;
    }

    private void DosageBoxShowHint_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox tb) return;

        DosageHint.Visibility = tb.Text == "" ? Visibility.Visible : Visibility.Collapsed;
    }

    private void QunatityBoxShowHint_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox tb) return;

        QuantityHint.Visibility = tb.Text == "" ? Visibility.Visible : Visibility.Collapsed;
    }

    private void QuantityBoxShowHint_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox cb) return;

        QuantityFormHint.Visibility = cb.SelectedIndex != -1 ? Visibility.Collapsed : Visibility.Visible;
    }

    private void ConsumptionBoxShowHint_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox cb) return;

        ConsumptionFormHint.Visibility = cb.SelectedIndex != -1 ? Visibility.Collapsed : Visibility.Visible;
    }

    private void PriceBoxShowHint_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox tb) return;

        PriceHint.Visibility = tb.Text == "" ? Visibility.Visible : Visibility.Collapsed;

    }

    private void ShowDescriptionHing_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox tb) return;

        DescriptionHint.Visibility = tb.Text == "" ? Visibility.Visible : Visibility.Collapsed;
    }
}