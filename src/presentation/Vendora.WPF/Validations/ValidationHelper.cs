﻿using System.Text.RegularExpressions;
using System.Text;
using System.Windows.Controls;

namespace Vendora.WPF.Validations;

public static class ValidationHelper
{
    public static void ValidateOnlyNumberInput(TextBox textBox)
    {
        if (textBox != null)
        {
            int caretIndex = textBox.CaretIndex;
            string cleanText = new string(textBox.Text.Where(char.IsDigit).ToArray());
            if (textBox.Text != cleanText)
            {
                textBox.Text = cleanText;
                textBox.CaretIndex = Math.Min(caretIndex, cleanText.Length);
            }
        }
    }

    public static void ValidatePasportInformation(TextBox textBox)
    {
        if (textBox == null) return;

        string inputText = textBox.Text.ToUpper();
        string filteredText = string.Empty;

        for (int i = 0; i < inputText.Length; i++)
        {
            if (i < 2 && char.IsLetter(inputText[i]) && char.IsUpper(inputText[i]))
            {
                filteredText += inputText[i];
            }
            else if (i >= 2 && i < 9 && char.IsDigit(inputText[i]))
            {
                filteredText += inputText[i];
            }
        }

        if (filteredText.Length > 9) filteredText = filteredText.Substring(0, 9);

        if (textBox.Text != filteredText)
        {
            int caretIndex = textBox.CaretIndex;
            textBox.Text = filteredText;
            textBox.CaretIndex = Math.Min(caretIndex, textBox.Text.Length);
        }
    }

    public static void ValidateAccountNumber(TextBox textBox)
    {
        if (textBox == null) return;

        string numericText = new string(textBox.Text.Where(char.IsDigit).ToArray());
        StringBuilder formattedText = new StringBuilder();
        int[] groupSizes = { 5, 3, 1, 8, 3 };
        int currentIndex = 0;

        foreach (int groupSize in groupSizes)
        {
            if (numericText.Length > currentIndex)
            {
                int charsToTake = Math.Min(groupSize, numericText.Length - currentIndex);
                formattedText.Append(numericText.Substring(currentIndex, charsToTake));
                currentIndex += charsToTake;
                if (currentIndex < numericText.Length) formattedText.Append('-');
            }
        }

        int oldCaretIndex = textBox.CaretIndex;

        if (textBox.Text != formattedText.ToString())
        {
            textBox.Text = formattedText.ToString();
            int newCaretIndex = oldCaretIndex;

            if (oldCaretIndex > 0 && oldCaretIndex < formattedText.Length && formattedText[oldCaretIndex - 1] == '-')
            {
                newCaretIndex++;
            }

            textBox.CaretIndex = Math.Min(newCaretIndex, textBox.Text.Length);
        }
    }

    public static void ValidateDecimalInput(TextBox textBox)
    {
        if (textBox == null) return;

        string inputText = textBox.Text;
        StringBuilder filteredText = new StringBuilder();
        bool hasDecimalSeparator = false;

        foreach (char c in inputText)
        {
            if (char.IsDigit(c))
            {
                filteredText.Append(c);
            }
            else if ((c == '.' || c == ',') && !hasDecimalSeparator)
            {
                filteredText.Append(c);
                hasDecimalSeparator = true;
            }
        }

        int oldCaretIndex = textBox.CaretIndex;

        if (textBox.Text != filteredText.ToString())
        {
            textBox.Text = filteredText.ToString();
            textBox.CaretIndex = Math.Min(oldCaretIndex, textBox.Text.Length);
        }
    }
    // Telefon raqamini UI da validatsiya qilish (rang o‘zgartirish)
    public static void ValidatePhone(TextBox textBox)
    {
        if (textBox == null) return;

        string text = textBox.Text?.Trim() ?? string.Empty;
        if (!string.IsNullOrEmpty(text) && !Regex.IsMatch(text, @"^\+998 \d{2} \d{7}$"))
        {
            textBox.Foreground = System.Windows.Media.Brushes.DarkRed;
        }
        else
        {
            textBox.Foreground = System.Windows.Media.Brushes.Black;
        }
    }

    // Telefon raqamini string sifatida validatsiya qilish
    public static bool IsValidPhone(string phone)
    {
        if (string.IsNullOrEmpty(phone)) return false; // Bo‘sh telefon raqami noto‘g‘ri
        return Regex.IsMatch(phone, @"^\+998 \d{2} \d{7}$");
    }


}
