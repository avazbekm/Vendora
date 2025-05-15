namespace Vendora.WPF;

using System.Windows;
using ApiServices.Services;
using Vendora.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;

public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; private set; } = default!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();

        // ApiService’dan API sozlamalarini olish
        ApiService.ConfigureServices(services, "https://localhost:7088/");

        // WPF xizmatlarini qo‘shish
        services.AddSingleton<MainViewModel>();

        ServiceProvider = services.BuildServiceProvider();

        var mainWindow = new MainWindow
        {
            DataContext = ServiceProvider.GetService<MainViewModel>()
        };
        mainWindow.Show();
    }
}