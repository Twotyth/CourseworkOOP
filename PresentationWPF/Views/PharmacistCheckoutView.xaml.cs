using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PresentationWPF.Views;

public partial class PharmacistCheckoutView : UserControl
{
    public PharmacistCheckoutView()
    {
        InitializeComponent();
    }

    private void ShowClientIdHint_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox tb) return;

        ClientHintLabel.Visibility = tb.Text.Any() ? Visibility.Collapsed : Visibility.Visible;
    }

    private void ShowSearchHint_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox tb) return;

        SearchHint.Visibility = tb.Text.Any() ? Visibility.Collapsed : Visibility.Visible;
    }
}