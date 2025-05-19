using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using Vendora.WPF.Validations;
using System.Text.RegularExpressions;

namespace Vendora.WPF.Windows.Users;

/// <summary>
/// Interaction logic for CreateUserWindow.xaml
/// </summary>
public partial class CreateUserWindow : Window
{
    public CreateUserWindow()
    {
        InitializeComponent();
        txtLastName.Focus();
    }

    private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
    {
        FormatPhoneNumber(txtPhone);
    }

    private void txtPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !Regex.IsMatch(e.Text, @"^\d+$"); // Faqat raqam kiritishga ruxsat
        base.OnPreviewTextInput(e);
    }

    private void txtPhone_GotFocus(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtPhone.Text) || !txtPhone.Text.StartsWith("+998"))
        {
            txtPhone.Text = "+998 ";
            txtPhone.SelectionStart = txtPhone.Text.Length;
        }
    }


    private void txtPassportSeries_TextChanged(object sender, TextChangedEventArgs e)
    {
        ValidationHelper.ValidatePasportInformation(sender as TextBox);
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {

    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void FormatPhoneNumber(TextBox textBox)
    {
        if (textBox == null) return;

        string text = textBox.Text?.Trim() ?? string.Empty;
        string digits = Regex.Replace(text, @"[^\d]", ""); // Raqamlar

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

    private void txtJshshir_TextChanged(object sender, TextChangedEventArgs e)
    {
        ValidationHelper.ValidateOnlyNumberInput(sender as TextBox);
    }
    // enter bosganda keyingi o'tish uchun yozilgan funksiya
    private void TextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            e.Handled = true;

            if (sender == txtLastName)
                txtFirstName.Focus();
            else if (sender == txtFirstName)
                cmbGender.Focus();
            else if (sender == cmbGender)
                txtPhone.Focus();
            else if (sender == txtPhone)
                txtLogin.Focus();
            else if (sender == txtLogin)
                txtPassword.Focus();
            else if (sender == txtPassword)
                cmbRole.Focus();
            else if (sender == cmbRole)
                txtPatronymic.Focus();
            else if (sender == txtPatronymic)
                txtPassportSeries.Focus();
            else if (sender == txtPassportSeries)
                dpDateOfBirth.Focus();
            else if (sender == dpDateOfBirth)
                dpDateOfIssue.Focus();
            else if (sender == dpDateOfIssue)
                dpDateOfExpiry.Focus();
            else if (sender == dpDateOfExpiry)
                txtAddress.Focus();
            else if (sender == txtAddress)
                txtJshshir.Focus();
            else if (sender == txtJshshir)
                btnSave.Focus(); // Oxirida Saqlash tugmasiga o‘tadi
        }
    }
  
}
