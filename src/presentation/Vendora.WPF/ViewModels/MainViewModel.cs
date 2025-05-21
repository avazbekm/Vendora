namespace Vendora.WPF.ViewModels;

using ApiServices.DTOs.Users;
using ApiServices.Interfaces;
using Refit;
using System.Collections.ObjectModel;
using System.Windows;

public class MainViewModel(IUsersApi usersApi)
{
    public ObservableCollection<User> Users { get; } = [];

    public async Task LoadUsersAsync()
    {
        try
        {
            var response = await usersApi.GetAllUsersAsync();
            if (response.IsSuccessStatusCode)
            {
                Users.Clear();
                foreach (var user in response.Content!)
                    Users.Add(user);

            }

            else
            {
                MessageBox.Show("Failed to load users from API.");
            }
        }
        catch (ApiException)
        {
            MessageBox.Show("An error occurred while connecting to the API.");
        }
    }
}