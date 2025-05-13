namespace Vendora.WPF.Windows.LoginWindow;

using System.Windows;

/// <summary>
/// Interaction logic for LoginWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        txtLogin.Focus();
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {

    }

    private void BtnEnter_Click(object sender, RoutedEventArgs e)
    {

    }
}
