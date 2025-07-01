using System.Windows;
using Vendora.WPF.Helpers;
using System.Windows.Input;
using System.Globalization;
using Vendora.WPF.ViewModels;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Vendora.WPF.UserControls;

/// <summary>
/// Логика взаимодействия для SuppliesControl.xaml
/// </summary>
public partial class SuppliesView : UserControl
{
    public SuppliesViewModel _supply = new SuppliesViewModel();
    private bool _isFirstKeyHandled = false;

    public SuppliesView()
    {
        InitializeComponent();
        Loaded += SuppliesView_Loaded;
        //DataContext = _supply;
        //AttachEnterKeyTraversal(this);
    }
    private void SuppliesView_Loaded(object sender, RoutedEventArgs e)
    {
        if (dateSupply != null)
        {
            // UserCalendar ichidagi dateTextBox’ga focus o‘rnatish
            var dateTextBox = dateSupply.FindName("dateTextBox") as TextBox;
            if (dateTextBox != null)
            {
                dateTextBox.Focus();
                Keyboard.Focus(dateTextBox);
            }
        }
    }
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var culture = CultureInfo.InvariantCulture;

        Goods _goods = new Goods()
        {
            TypeGoods = typeGoods.Text,
            NameGoods = nameGoods.Text,
            AmountGoods = amountGoods.Text,
            UnitGoods = unitGoods.Text,
            PriceGoods = priceGoods.Text,
            RateVAT = decimal.TryParse(rateVAT.Text, NumberStyles.Any, culture, out decimal vat) ? vat : (decimal?)null,
            PriceGoodsVAT = decimal.TryParse(priceGoodsVAT.Text, NumberStyles.Any, culture, out decimal priceVat) ? priceVat : (decimal?)null,
            PriceSum = priceSum.Text,
            PriceSell = priceSell.Text,
            PriceSellVAT = priceSellVAT.Text
        };
        _supply.GoodsList1.Insert(0, _goods); // Добавляем новый элемент в начало коллекции
        MessageBox.Show(_supply.PriceSum);
        _supply.TotalSum = (decimal)_supply.GoodsList1.Sum(x =>
        {
            if (decimal.TryParse(x.PriceSum.Replace(" ", ""), NumberStyles.Any, culture, out decimal result))
            {
                return result;
            }
            return 0;
        });
        typeGoods.Text = null;
        nameGoods.Text = null;
        amountGoods.Text = null;
        unitGoods.Text = null;
        priceGoods.Text = null;
        rateVAT.Text = null;
        priceGoodsVAT.Text = null;
        priceSum.Text = null;
        priceSell.Text = null;
        priceSellVAT.Text = null;
        FocusMovement.MoveFocusToElement("typeGoods", this);
    }

    private void dataGridGoods_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        var grid = (DataGrid)sender;

        // Только для NumericColumn
        if (!(grid.CurrentColumn is NumericColumn)) return;

        // Только если НЕ редактируется ещё
        if (!_isFirstKeyHandled)
        {
            _isFirstKeyHandled = true;

            // Запускаем редактирование
            grid.BeginEdit();

            // Ждём появления TextBox и вставляем первую клавишу
            grid.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (grid.CurrentColumn.GetCellContent(grid.CurrentItem) is TextBox tb)
                {
                    tb.Text = ""; // очищаем текст
                    tb.Focus();
                    tb.Text = e.Text;
                    tb.CaretIndex = tb.Text.Length;
                }
            }), DispatcherPriority.Background);

            e.Handled = true;
        }
    }

    private void dataGridGoods_CurrentCellChanged(object sender, EventArgs e)
    {
        _isFirstKeyHandled = false;
    }

    private void dataGridGoods_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
    {
        _isFirstKeyHandled = false;

    }
}
