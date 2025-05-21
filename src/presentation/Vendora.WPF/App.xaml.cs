namespace Vendora.WPF;

using System.Windows;
using ApiServices.Services;
using Vendora.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Vendora.WPF.Windows.Users;
using Vendora.WPF.Windows.LoginWindow;

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
        services.AddSingleton<MainViewModel2>();
        services.AddSingleton<LoginViewModel>();

        ServiceProvider = services.BuildServiceProvider();

        ServiceProvider = services.BuildServiceProvider();

        var loginWindow = new LoginWindow2
        {
            DataContext = ServiceProvider.GetService<LoginViewModel>()
        };
        loginWindow2.Show();
        loginWindow2.IsVisibleChanged += (s, e) =>
        {
            if (loginWindow2.IsVisible == false && loginWindow2.IsLoaded)
            {
                var mainWindow2 = new MainWindow2
                {
                    DataContext = ServiceProvider.GetService<MainWindow2>()
                };
                mainWindow2.Show();
                loginWindow2.Close();
            }
        };

    }
}