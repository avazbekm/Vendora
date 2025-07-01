using System.Windows;
using ApiServices.Interfaces;
using System.Windows.Controls;
using Vendora.WPF.Windows.Users;
using Microsoft.Extensions.DependencyInjection;
using ApiServices.DTOs.Users;

namespace Vendora.WPF.UserControls;

/// <summary>
/// Логика взаимодействия для CustomView.xaml
/// </summary>
public partial class CustomView : UserControl
{
    private readonly IServiceProvider services;
    private readonly IUsersApi usersApi;
    public CustomView(IServiceProvider services)
    {
        InitializeComponent();
        this.services = services;
        usersApi = services.GetRequiredService<IUsersApi>();
        Loaded += CustomView_Loaded;
    }

    private async void CustomView_Loaded(object sender, RoutedEventArgs e)
    {
        await LoadUsersAsync();
    }

    private async Task LoadUsersAsync()
    {
        try
        {
            var response = await usersApi.GetAllUsersAsync();
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                dgUsers.ItemsSource = response.Content;
            }
            else
            {
                MessageBox.Show("Foydalanuvchilar ro‘yxatini olishda xatolik yuz berdi: " +
                    (response.Error?.Content ?? "Ma'lumotlar topilmadi"),
                    "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Xatolik: {ex.Message}", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    private void btnAddUser_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var addUserWindow = new AddUserWindow(services);
        addUserWindow.ShowDialog();
    }

    private async void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = tbSearch.Text.ToLower();
        try
        {
            var response = await usersApi.GetAllUsersAsync();
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                var filteredUsers = response.Content
                    .Where(u => u.FirstName.ToLower().Contains(searchText) ||
                                u.LastName.ToLower().Contains(searchText) ||
                                u.Phone.ToLower().Contains(searchText));
                dgUsers.ItemsSource = filteredUsers;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Qidiruvda xatolik: {ex.Message}", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void btnDeleteUser_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is User user)
        {
            var result = MessageBox.Show($"Foydalanuvchi {user.FirstName} {user.LastName} o‘chirilsinmi?",
                                         "O‘chirishni tasdiqlash",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var response = await usersApi.DeleteUserAsync(user.Id);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Foydalanuvchi muvaffaqiyatli o‘chirildi.",
                                        "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);
                        await LoadUsersAsync();
                    }
                    else
                    {
                        MessageBox.Show($"O‘chirishda xatolik: {response.Error?.Content ?? "Noma'lum xato"}",
                                        "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Xatolik: {ex.Message}", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }

    private void btnEditUser_Click(object sender, RoutedEventArgs e)
    {

    }
}
