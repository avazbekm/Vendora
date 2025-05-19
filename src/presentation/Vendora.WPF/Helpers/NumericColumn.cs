using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Vendora.WPF.Helpers
{
    public class NumericColumn : DataGridTemplateColumn
    {
        public string BindingPath { get; set; }
        public string StringFormat { get; set; } = "N2";

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            var textBlock = new TextBlock
            {
                TextAlignment = TextAlignment.Right,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            var binding = new Binding(BindingPath)
            {
                StringFormat = StringFormat
            };
            textBlock.SetBinding(TextBlock.TextProperty, binding);

            //// Клавиша сразу начинает редактирование
            //textBlock.PreviewKeyDown += (s, e) =>
            //{
            //    var dg = FindParent<DataGrid>(textBlock);
            //    if (dg != null && !cell.IsEditing)
            //    {
            //        dg.CurrentCell = new DataGridCellInfo(dataItem, this);
            //        dg.BeginEdit();

            //        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            //        {
            //            if (GetEditingTextBox(cell) is TextBox tb)
            //            {
            //                tb.Text = "";
            //                tb.CaretIndex = 0;
            //                tb.Text += GetCharFromKey(e.Key);
            //                tb.CaretIndex = tb.Text.Length;
            //            }
            //        }));
            //        e.Handled = true;
            //    }
            //};

            return textBlock;
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            var textBox = new TextBox
            {
                TextAlignment = TextAlignment.Right,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            Helpers.NumericTextBox.SetIsNumeric(textBox, true);

            textBox.GotKeyboardFocus += (s, e) =>
            {
                //textBox.Dispatcher.BeginInvoke(() => textBox.SelectAll());
            };

            var binding = new Binding(BindingPath)
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                NotifyOnTargetUpdated = true,
                ValidatesOnDataErrors = true,
                ValidatesOnExceptions = true
            };

            textBox.SetBinding(TextBox.TextProperty, binding);

            return textBox;
        }

        //private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        //{
        //    while (child != null)
        //    {
        //        if (child is T parent)
        //            return parent;
        //        child = VisualTreeHelper.GetParent(child);
        //    }
        //    return null;
        //}

        //private static TextBox GetEditingTextBox(DataGridCell cell)
        //{
        //    return FindVisualChild<TextBox>(cell);
        //}

        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild)
                    return typedChild;
                var result = FindVisualChild<T>(child);
                if (result != null)
                    return result;
            }
            return null;
        }

        //// Преобразование Key в char (только для цифр и букв)
        //private static string GetCharFromKey(Key key)
        //{
        //    // Простая реализация для цифр
        //    if (key >= Key.D0 && key <= Key.D9)
        //        return ((char)('0' + (key - Key.D0))).ToString();
        //    if (key >= Key.NumPad0 && key <= Key.NumPad9)
        //        return ((char)('0' + (key - Key.NumPad0))).ToString();

        //    return string.Empty; // можно расширить для букв и символов
        //}
    }
}

