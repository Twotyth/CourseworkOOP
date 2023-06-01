using System.Windows;
using System.Windows.Controls;

namespace PresentationWPF.Views;

public partial class ClientCabinetView : UserControl
{
    public ClientCabinetView()
    {
        InitializeComponent();
    }
    
    private void ShowUsernameHint_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox tb) return;
        LoginHintLabel.Visibility = string.IsNullOrEmpty(tb.Text) ? Visibility.Visible : Visibility.Collapsed;
    }

    private void ShowPasswordHintBindPassword_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is not PasswordBox pb) return;
        
        if (DataContext != null)
        {
            ((dynamic)DataContext).NewPassword = pb.Password;
        }
        
        PasswordHintLabel.Visibility = string.IsNullOrEmpty(pb.Password) 
            ? Visibility.Visible
            : Visibility.Hidden;
    }

    private void ShowRepeatPasswordHintBindRepeatPassword_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is not PasswordBox pb) return;
        
        if (DataContext != null)
        {
            ((dynamic)DataContext).NewPasswordRepeat = pb.Password;
        }
        
        RepeatPasswordHintLabel.Visibility = string.IsNullOrEmpty(pb.Password) 
            ? Visibility.Visible
            : Visibility.Hidden;
    }
}