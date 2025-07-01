namespace Vendora.WPF;

using System.Windows;
using ApiServices.Services;
using Vendora.WPF.ViewModels;
using Vendora.WPF.Windows.LoginWindow;
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
        services.AddSingleton<LoginViewModel>();


        var serviceProvider = services.BuildServiceProvider();

        var loginWindow = new LoginWindow
        {
            DataContext = serviceProvider.GetService<LoginViewModel>()
        };
        loginWindow.Show();
        loginWindow.IsVisibleChanged += (s, e) =>
        {
            if (loginWindow.IsVisible == false && loginWindow.IsLoaded)
            {
                var mainWindow2 = new MainWindow
                {
                    DataContext = serviceProvider.GetService<MainViewModel>()
                };
                mainWindow2.Show();
                loginWindow.Close();
            }
        };

    }
}