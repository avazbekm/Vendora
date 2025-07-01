using FontAwesome.Sharp;
using Vendora.WPF.Models;
using System.Windows.Input;
using Vendora.WPF.UserControls;

namespace Vendora.WPF.ViewModels;

public class MainViewModel : ViewModelBase
{
    // Fields
    private UserAccountModel _currentAccountUser;
    private object _currentChildView;
    private string _caption;
    private IconChar _iconCache;

    public UserAccountModel CurrentAccountUser
    {
        get => _currentAccountUser;
        set
        {
            _currentAccountUser = value;
            OnPropertyChanged(nameof(CurrentAccountUser));
        }
    }
    // Properties
    public object CurrentChildView
    {
        get => _currentChildView;
        set
        {
            _currentChildView = value;
            OnPropertyChanged(nameof(CurrentChildView));
        }
    }
    public string Caption
    {
        get => _caption;
        set
        {
            _caption = value;
            OnPropertyChanged(nameof(Caption));
        }
    }
    public IconChar IconCache
    {
        get => _iconCache;
        set
        {
            _iconCache = value;
            OnPropertyChanged(nameof(IconCache));
        }
    }

    // Кэшированные ViewModels (один раз создаются)

    private readonly CustomViewModel _customViewModel = new CustomViewModel();
    private readonly SuppliesViewModel _suppliesViewModel = new SuppliesViewModel();
    private readonly ProductViewModel _productViewModel = new ProductViewModel();

    // Commands
    public ICommand ShowHomeViewCommand { get; }
    public ICommand ShowCustomViewCommand { get; }
    public ICommand ShowSuppliesViewCommand { get; }
    public ICommand ShowProductViewCommand { get; }

    // Constructor
    public MainViewModel() 
    {
        CurrentAccountUser = new UserAccountModel();
        LoadCurrentUserData();
        // Initialize commands
        ShowCustomViewCommand = new ViewModelCommand(ExecuteShowCustomViewCommand);
        ShowSuppliesViewCommand = new ViewModelCommand(ExecuteShowSuppliesViewCommand);
        ShowProductViewCommand = new ViewModelCommand(ExecuteShowProductViewCommand);
    }

    private readonly IServiceProvider _serviceProvider;

    public MainViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        CurrentAccountUser = new UserAccountModel();
        LoadCurrentUserData();
        // Initialize commands
        ShowCustomViewCommand = new ViewModelCommand(ExecuteShowCustomViewCommand);
        ShowSuppliesViewCommand = new ViewModelCommand(ExecuteShowSuppliesViewCommand);
        ShowProductViewCommand = new ViewModelCommand(ExecuteShowProductViewCommand);

    }



    private void ExecuteShowSuppliesViewCommand(object obj)
    {
        var view = new SuppliesView { DataContext = _suppliesViewModel };
        
        CurrentChildView = view;
        Caption = "Kirim";
        IconCache = IconChar.TruckMoving;
    }

    private void ExecuteShowCustomViewCommand(object obj)
    {
        var view = new CustomView(_serviceProvider) { DataContext = _customViewModel };
        CurrentChildView = view;
        Caption = "Hodimlar";
        IconCache = IconChar.UserGroup;
    }

    private void ExecuteShowProductViewCommand(object obj)
    {
        var view = new ProductView { DataContext = _productViewModel };
        CurrentChildView = view;
        Caption = "Mahsulotlar";
        IconCache = IconChar.BoxOpen;
    }

    private void LoadCurrentUserData()
    {
        var user = AuthService.GetByUserName(Thread.CurrentPrincipal.Identity.Name);
        if (user != null)
        {

            CurrentAccountUser.Username = user.Username;
            CurrentAccountUser.DisplayName = $"{user.Name} {user.LastName} {user.Email}";
            CurrentAccountUser.ProfilePicture = null!;
        }
        else
        {
            CurrentAccountUser.DisplayName = "Invalid user, not logged in";
        }
    }
}
