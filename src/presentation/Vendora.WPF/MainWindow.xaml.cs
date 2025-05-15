namespace Vendora.WPF;

using System.Windows;
using Vendora.WPF.ViewModels;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}