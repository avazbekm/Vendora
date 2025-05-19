using System.Windows;
using Microsoft.Win32;
using System.Windows.Input;
using ApiServices.DTOs.Users;
using ApiServices.Interfaces;
using System.Windows.Controls;
using Vendora.WPF.Validations;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;

namespace Vendora.WPF.Windows.Users;

public partial class AddUserWindow : Window
{
    private bool isSwitched = false;
    private CornerRadius leftPanelCornerRadius;
    private CornerRadius rightPanelCornerRadius;
    // parolda ko'zni ochib yopish uchun
    private bool isPasswordVisible = false;
    private readonly IUsersApi _usersApi;
    private IServiceProvider services;
    public AddUserWindow(IServiceProvider services)
    {
        InitializeComponent();
        this.services = services;
        _usersApi = services.GetRequiredService<IUsersApi>();
        leftPanelCornerRadius = LeftPanel.CornerRadius;
        rightPanelCornerRadius = RightPanel.CornerRadius;
        txtLastName.Focus();
        cbPosition.Items.Add(new ComboBoxItem { Content = "Administrator" });
        cbPosition.Items.Add(new ComboBoxItem { Content = "User" });
        cbPosition.Items.Add(new ComboBoxItem { Content = "Manager" });
        this.services = services;
    }

    private void btnRight_Click(object sender, RoutedEventArgs e)
    {
        if (!isSwitched)
        {
            Grid.SetColumn(LeftPanel, 1);
            Grid.SetColumn(RightPanel, 0);
            var tempWidth = LayoutRoot.ColumnDefinitions[0].Width;
            LayoutRoot.ColumnDefinitions[0].Width = LayoutRoot.ColumnDefinitions[1].Width;
            LayoutRoot.ColumnDefinitions[1].Width = tempWidth;
            LeftPanel.CornerRadius = rightPanelCornerRadius;
            RightPanel.CornerRadius = leftPanelCornerRadius;
            AdditionalFieldsPanel.Visibility = Visibility.Visible;
            SetMainFieldsVisibility(Visibility.Collapsed);
            btnRight.Content = "🔙";
            txtFatherName.Focus();
            isSwitched = true;
        }
        else
        {
            Grid.SetColumn(LeftPanel, 0);
            Grid.SetColumn(RightPanel, 1);
            var tempWidth = LayoutRoot.ColumnDefinitions[0].Width;
            LayoutRoot.ColumnDefinitions[0].Width = LayoutRoot.ColumnDefinitions[1].Width;
            LayoutRoot.ColumnDefinitions[1].Width = tempWidth;
            LeftPanel.CornerRadius = leftPanelCornerRadius;
            RightPanel.CornerRadius = rightPanelCornerRadius;
            AdditionalFieldsPanel.Visibility = Visibility.Collapsed;
            SetMainFieldsVisibility(Visibility.Visible);
            btnRight.Content = "🔜";
            isSwitched = false;
        }
    }

    private void SetMainFieldsVisibility(Visibility visibility)
    {
        spLastname.Visibility = visibility;
        spFirstName.Visibility = visibility;
        spGender.Visibility = visibility;
        spRole.Visibility = visibility;
        spPhone.Visibility = visibility;
        spLogin.Visibility = visibility;
        spPassword.Visibility = visibility;
    }

    private void UploadImage_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Title = "Rasm tanlash",
            Filter = "Rasm fayllari|*.jpg;*.jpeg;*.png;*.bmp"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            string selectedFilePath = openFileDialog.FileName;
            try
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = new Uri(selectedFilePath);
                bitmap.EndInit();
                AvatarImageBrush.ImageSource = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Rasmni yuklashda xatolik: {ex.Message}", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private async void Save_Click(object sender, RoutedEventArgs e)
    {
        // Majburiy maydonlarni tekshirish
        if (string.IsNullOrWhiteSpace(txtLastName.Text))
        {
            MessageBox.Show("Familiya kiritilishi shart!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            txtLastName.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtFirstName.Text))
        {
            MessageBox.Show("Ism kiritilishi shart!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            txtFirstName.Focus();
            return;
        }
        if (!rbMale.IsChecked.Value && !rbFemale.IsChecked.Value)
        {
            MessageBox.Show("Jins tanlanishi shart!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            rbMale.Focus();
            return;
        }
        if (cbPosition.SelectedItem == null)
        {
            MessageBox.Show("Lavozim tanlanishi shart!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            cbPosition.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtPhone.Text) || txtPhone.Text == "+998 ")
        {
            MessageBox.Show("Telefon raqami kiritilishi shart!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            txtPhone.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtLogin.Text))
        {
            MessageBox.Show("Login kiritilishi shart!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            txtLogin.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtPassword.Password))
        {
            MessageBox.Show("Parol kiritilishi shart!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            txtPassword.Focus();
            return;
        }

        // User DTO’ni to‘ldirish
        var user = new User
        {
            LastName = txtLastName.Text.Trim().ToLower(),
            FirstName = txtFirstName.Text.Trim().ToLower(),
            Patronomyc = txtFatherName.Text.Trim().ToLower(),
            Address = txtAddress.Text.Trim().ToLower(),
            JShShIR = txtJSHIR.Text.Trim(),
            PasportSeria = txtPassportSeries.Text.Trim(),
            Login = txtLogin.Text.Trim(),
            Password = txtPassword.Password.Trim(),
            Phone = txtPhone.Text.Trim().Replace(" ", ""),
            Gender = rbMale.IsChecked.Value ? 1 : 2, // Erkak: 1, Ayol: 2
            RoleId = cbPosition.SelectedItem switch
            {
                ComboBoxItem item when item.Content.ToString() == "Administrator" => 1,
                ComboBoxItem item when item.Content.ToString() == "User" => 2,
                ComboBoxItem item when item.Content.ToString() == "Manager" => 3,
                _ => 0 // Agar xato bo‘lsa
            },
            PhotoId = null, // Hozircha rasm API’ga yuborilmaydi
        };

        // Sana maydonlarini konvertatsiya qilish
        try
        {
            if (!string.IsNullOrWhiteSpace(txtBirthDate.Text) &&
                DateTimeOffset.TryParseExact(txtBirthDate.Text, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out var birthDate))
            {
                user.DateOfBirth = birthDate.UtcDateTime;
            }
            if (!string.IsNullOrWhiteSpace(txtIssueDate.Text) &&
                DateTimeOffset.TryParseExact(txtIssueDate.Text, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out var issueDate))
            {
                user.DateOfIssue = issueDate.UtcDateTime;
            }
            if (!string.IsNullOrWhiteSpace(txtExpireDate.Text) &&
                DateTimeOffset.TryParseExact(txtExpireDate.Text, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out var expireDate))
            {
                user.DateOfExpiry = expireDate.UtcDateTime;
            }
        }
        catch (FormatException)
        {
            MessageBox.Show("Sana formatida xatolik (dd.MM.yyyy kutilmoqda)!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        // API so‘rovi
        try
        {
            var response = await _usersApi.CreateUserAsync(user);
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                MessageBox.Show("Foydalanuvchi muvaffaqiyatli yaratildi!", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show($"Xatolik yuz berdi: {response.Error?.Message ?? "Noma'lum xato"}", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"API so‘rovida xatolik: {ex.Message}", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void TextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            e.Handled = true;
            if (sender == txtLastName)
                txtFirstName.Focus();
            else if (sender == txtFirstName)
                rbMale.Focus();
            else if (sender == rbMale)
                rbFemale.Focus();
            else if (sender == rbFemale)
                cbPosition.Focus();
            else if (sender == cbPosition)
                txtPhone.Focus();
            else if (sender == txtPhone)
                txtLogin.Focus();
            else if (sender == txtLogin)
                txtPassword.Focus();
            else if (sender == txtPassword)
                btnRight.Focus();
            else if (sender == txtFatherName)
                txtAddress.Focus();
            else if (sender == txtAddress)
                txtBirthDate.Focus();
            else if (sender == txtBirthDate)
                txtJSHIR.Focus();
            else if (sender == txtJSHIR)
                txtPassportSeries.Focus();
            else if (sender == txtPassportSeries)
                txtIssueDate.Focus();
            else if (sender == txtIssueDate)
                txtExpireDate.Focus();
            else if (sender == txtExpireDate)
                btnSave.Focus();
        }
    }

    private void cbPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cbPosition.SelectedItem != null)
        {
            cbPosition.IsDropDownOpen = false;
            Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
            {
                Keyboard.ClearFocus();
                txtPhone.Focus();
            }));
        }
    }

    private void DateTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !Regex.IsMatch(e.Text, @"[\d\.]");
    }

    private void DateTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var tb = sender as TextBox;
        string digits = Regex.Replace(tb.Text, @"[^\d]", "");
        if (digits.Length > 8)
            digits = digits.Substring(0, 8);
        string formatted = FormatDate(digits);
        tb.TextChanged -= DateTextBox_TextChanged;
        tb.Text = formatted;
        tb.CaretIndex = tb.Text.Length;
        tb.TextChanged += DateTextBox_TextChanged;
    }

    private string FormatDate(string input)
    {
        if (input.Length <= 2)
            return input;
        else if (input.Length <= 4)
            return input.Insert(2, ".");
        else if (input.Length <= 8)
            return input.Insert(2, ".").Insert(5, ".");
        else
            return input;
    }

    private void btnIssueCalendar_Click(object sender, RoutedEventArgs e)
    {
        popupIssueDate.IsOpen = true;
    }

    private void calendarIssueDate_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
    {
        if (calendarIssueDate.SelectedDate is DateTime selectedDate)
        {
            txtIssueDate.Text = selectedDate.ToString("dd.MM.yyyy");
        }
        popupIssueDate.IsOpen = false;
    }

    private void btnExpireCalendar_Click(object sender, RoutedEventArgs e)
    {
        popupExpireDate.IsOpen = true;
    }

    private void calendarExpireDate_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
    {
        if (calendarExpireDate.SelectedDate is DateTime selectedDate)
        {
            txtExpireDate.Text = selectedDate.ToString("dd.MM.yyyy");
        }
        popupExpireDate.IsOpen = false;
    }

    private void btnBirthCalendar_Click(object sender, RoutedEventArgs e)
    {
        popupBirthDate.IsOpen = true;
    }

    private void calendarBirthDate_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
    {
        if (calendarBirthDate.SelectedDate is DateTime selectedDate)
        {
            txtBirthDate.Text = selectedDate.ToString("dd.MM.yyyy");
        }
        popupBirthDate.IsOpen = false;
    }

    private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
    {
        FormatPhoneNumber(sender as TextBox);
    }

    private void txtPhone_GotFocus(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtPhone.Text) || !txtPhone.Text.StartsWith("+998"))
        {
            txtPhone.Text = "+998 ";
            txtPhone.SelectionStart = txtPhone.Text.Length;
        }
    }

    private void FormatPhoneNumber(TextBox textBox)
    {
        if (textBox == null) return;
        string text = textBox.Text?.Trim() ?? string.Empty;
        string digits = Regex.Replace(text, @"[^\d]", "");
        textBox.TextChanged -= txtPhone_TextChanged;
        try
        {
            if (!digits.StartsWith("998"))
            {
                digits = "998";
            }
            string formatted = "+998 ";
            if (digits.Length > 3)
            {
                formatted += digits.Substring(3, Math.Min(2, digits.Length - 3));
            }
            if (digits.Length > 5)
            {
                formatted += " " + digits.Substring(5, Math.Min(3, digits.Length - 5));
            }
            if (digits.Length > 8)
            {
                formatted += " " + digits.Substring(8, Math.Min(2, digits.Length - 8));
            }
            if (digits.Length > 10)
            {
                formatted += " " + digits.Substring(10, Math.Min(2, digits.Length - 10));
            }
            textBox.Text = formatted.TrimEnd();
            textBox.SelectionStart = textBox.Text.Length;
        }
        finally
        {
            textBox.TextChanged += txtPhone_TextChanged;
        }
    }

    private void txtJSHIR_TextChanged(object sender, TextChangedEventArgs e)
    {
        ValidationHelper.ValidateOnlyNumberInput(sender as TextBox);
    }

    private void txtPassportSeries_TextChanged(object sender, TextChangedEventArgs e)
    {
        ValidationHelper.ValidatePasportInformation(sender as TextBox);
    }

    private void btnEye_Click(object sender, RoutedEventArgs e)
    {
        if (isPasswordVisible)
        {
            txtPassword.Password = txtPasswordVisible.Text;
            txtPassword.Visibility = Visibility.Visible;
            txtPasswordVisibleBorder.Visibility = Visibility.Collapsed;
            btnEye.Content = "🙈";
            isPasswordVisible = false;
        }
        else
        {
            txtPasswordVisible.Text = txtPassword.Password;
            txtPassword.Visibility = Visibility.Collapsed;
            txtPasswordVisibleBorder.Visibility = Visibility.Visible;
            btnEye.Content = "🙉";
            isPasswordVisible = true;
        }
    }

    private void cbPosition_GotFocus(object sender, RoutedEventArgs e)
    {
        cbPosition.IsDropDownOpen = true;
    }
}