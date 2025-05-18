using FontAwesome.Sharp;
using System.Windows.Input;
using Vendora.WPF.Models;

namespace Vendora.WPF.ViewModels
{
    public class MainViewModel2 : ViewModelBase
    {
        // Fields
        private UserAccountModel _currentAccountUser;
        private ViewModelBase _currentChildView;
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
        public ViewModelBase CurrentChildView
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

        // Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCustomViewCommand { get; }
        // Constructor
        public MainViewModel2()
        {
            CurrentAccountUser = new UserAccountModel();
            LoadCurrentUserData();
            // Initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowCustomViewCommand = new ViewModelCommand(ExecuteShowCustomViewCommand);
            // Set default view
            ExecuteShowHomeViewCommand(null!);
        }


        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "DashBoard";
            IconCache = IconChar.Home;
        }
        private void ExecuteShowCustomViewCommand(object obj)
        {
            CurrentChildView = new CustomViewModel();
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
