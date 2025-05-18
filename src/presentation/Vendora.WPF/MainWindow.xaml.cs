namespace Vendora.WPF;

using System.Windows;
using Vendora.WPF.ViewModels;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void LoadUsers_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
            await viewModel.LoadUsersAsync();
    }
}