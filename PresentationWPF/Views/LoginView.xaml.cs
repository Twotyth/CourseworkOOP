using System.Windows;
using System.Windows.Controls;

namespace PresentationWPF.Views;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
    }

    private void ShowLoginHint_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox tb) return;
        LoginHintLabel.Visibility = string.IsNullOrEmpty(tb.Text) 
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    private void ShowPasswordHintBindPassword_OnPasswordChanged(object sender, RoutedEventArgs routedEventArgs)
    {
        if (sender is not PasswordBox pb) return;
        
        if (this.DataContext != null)
        {
            ((dynamic)this.DataContext).Password = pb.Password;
        }
        
        PasswordHintLabel.Visibility = string.IsNullOrEmpty(pb.Password) 
            ? Visibility.Visible
            : Visibility.Collapsed;
    }
}