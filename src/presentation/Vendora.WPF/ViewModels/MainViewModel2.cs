using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendora.WPF.Models;
using System.Globalization;
using System.Windows.Input;

namespace Vendora.WPF.ViewModels
{
    public class MainViewModel2 : ViewModelBase
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
        private readonly HomeViewModel _homeViewModel = new HomeViewModel();
        private readonly SuppliesViewModel _suppliesViewModel = new SuppliesViewModel();
        // Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCustomViewCommand { get; }
        public ICommand ShowSuppliesViewCommand { get; }
        // Constructor
        public MainViewModel2()
        {
            CurrentAccountUser = new UserAccountModel();
            LoadCurrentUserData();
            // Initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowCustomViewCommand = new ViewModelCommand(ExecuteShowCustomViewCommand);
            ShowSuppliesViewCommand = new ViewModelCommand(ExecuteShowSuppliesViewCommand);
            // Set default view
            ExecuteShowHomeViewCommand(null!);
        }

        private void ExecuteShowSuppliesViewCommand(object obj)
        {
            var view = new SuppliesView { DataContext = _suppliesViewModel };
            CurrentChildView = view;
            Caption = "Ta'minot";
            IconCache = IconChar.TruckMoving;
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            var view = new HomeView {DataContext=_homeViewModel};
            CurrentChildView = view;
            Caption = "DashBoard";
            IconCache = IconChar.Home;
        }
        private void ExecuteShowCustomViewCommand(object obj)
        {
            var view = new CustomView { DataContext= _customViewModel};
            CurrentChildView = view;
            Caption = "Savdo";
            IconCache = IconChar.UserGroup;
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
}
