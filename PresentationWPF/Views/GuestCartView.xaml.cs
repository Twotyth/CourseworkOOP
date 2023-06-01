using System.Windows;
using System.Windows.Controls;

namespace PresentationWPF.Views;

public partial class GuestCartView : UserControl
{
    public GuestCartView()
    {
        InitializeComponent();
    }

    private void ShowPrescriptionHint_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox tb) return;
        PrescriptionHintLabel.Visibility = string.IsNullOrEmpty(tb.Text) ? Visibility.Visible : Visibility.Collapsed;
    }
}