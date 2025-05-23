﻿using System.Windows;
using System.Windows.Input;

namespace Vendora.WPF.Helpers
{
    public static class EnterKeyTraversal
    {
        static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(EnterKeyTraversal),
                new UIPropertyMetadata(false, OnIsEnabledChanged));

        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                if ((bool)e.NewValue)
                {
                    element.PreviewKeyDown += Element_PreviewKeyDown;
                }
                else
                {
                    element.PreviewKeyDown -= Element_PreviewKeyDown;
                }
            }
        }
        private static void Element_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true; // предотвращаем стандартное поведение

                // Создаем событие, эмулирующее нажатие клавиши Tab
                KeyEventArgs tabKeyEvent = new KeyEventArgs(
                    Keyboard.PrimaryDevice,
                    Keyboard.PrimaryDevice.ActiveSource,
                    0,
                    Key.Tab)
                {
                    RoutedEvent = Keyboard.KeyDownEvent
                };

                // Отправляем событие текущему элементу
                InputManager.Current.ProcessInput(tabKeyEvent);
            }
        }
    }
    public static class FocusMovement
    {
        // Метод для перевода фокуса по имени
        public static void MoveFocusToElement(string elementName, DependencyObject parent)
        {
            Window window = Window.GetWindow(parent);
            if (window != null)
            {
                UIElement targetElement = window.FindName(elementName) as UIElement;
                if (targetElement != null)
                {
                    targetElement.Focus();
                }
            }
        }

    }
}