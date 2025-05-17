using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendora.WPF.Models;

namespace Vendora.WPF.ViewModels
{
    public class MainViewModel2 : ViewModelBase
    {
        // Fields
        private UserAccountModel _currentAccountUser;

        public UserAccountModel CurrentAccountUser
        {
            get => _currentAccountUser;
            set
            {
                _currentAccountUser = value;
                OnPropertyChanged(nameof(CurrentAccountUser));
            }
        }

        public MainViewModel2()
        {
            CurrentAccountUser = new UserAccountModel();
            LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {
            var user = AuthService.GetByUserName(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {

                CurrentAccountUser.Username = user.Username;
                CurrentAccountUser.DisplayName = $"{user.Name} {user.LastName}";
                CurrentAccountUser.ProfilePicture = null;
            }
            else
            {
                CurrentAccountUser.DisplayName = "Invalid user, not logged in";
            }
        }
    }
}
