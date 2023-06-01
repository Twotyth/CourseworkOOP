using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PresentationWPF.Views;

public partial class NewPharmacistView : UserControl
{
    public NewPharmacistView()
    {
        InitializeComponent();
    }

    private void ShowSalaryHint_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox tb) return;

        SalaryHint.Visibility = tb.Text.Any() ? Visibility.Collapsed : Visibility.Visible;
    }

    private void ShowLoginHint_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox tb) return;

        LoginHint.Visibility = tb.Text.Any() ? Visibility.Collapsed : Visibility.Visible;
    }

    private void ShowPasswordHint_OnTextChanged(object sender, RoutedEventArgs e)
    {
        if (sender is not PasswordBox pb) return;
        try
        {
            ((dynamic)DataContext).Password = pb.Password;
        }
        catch
        {
            // ignored
        }

        PasswordHint.Visibility = pb.Password.Any() ? Visibility.Collapsed : Visibility.Visible;
    }
}