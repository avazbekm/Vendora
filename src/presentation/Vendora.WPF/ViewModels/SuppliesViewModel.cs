using System.Collections.ObjectModel;

namespace Vendora.WPF.ViewModels;

public class SuppliesViewModel : ViewModelBase
{
    private decimal? _totalSum;
    public DateTime DateSupply { get; set; } = DateTime.Now;
    public string DocNumber { get; set; } = string.Empty;
    public string SupplierName { get; set; } = string.Empty;
    public string CurrencyType { get; set; } = string.Empty;
    public decimal? TotalSum
    {
        get => _totalSum;
        set
        {
            //if (_totalSum != value)
            //{
            _totalSum = value;
            OnPropertyChanged(nameof(TotalSum));
            //}
        }
    }
    public string Info { get; set; } = string.Empty;
    public string TypeGoods { get; set; } = string.Empty;
    public string NameGoods { get; set; } = string.Empty;
    public string? AmountGoods { get; set; }
    public string UnitGoods { get; set; } = string.Empty;
    public string? PriceGoods { get; set; }
    public decimal? RateVAT { get; set; }
    public decimal? PriceGoodsVAT { get; set; }
    public string? PriceSum { get; set; }
    public string? PriceSell { get; set; }
    public string? PriceSellVAT { get; set; }
    public ObservableCollection<Goods> GoodsList { get; set; } = new ObservableCollection<Goods>();
    public List<Goods> GoodsList1 { get; set; } = new List<Goods>();
}
public class Goods
{
    public string TypeGoods { get; set; } = string.Empty;
    public string NameGoods { get; set; } = string.Empty;
    public string? AmountGoods { get; set; }
    public string UnitGoods { get; set; } = string.Empty;
    public string? PriceGoods { get; set; }
    public decimal? RateVAT { get; set; }
    public decimal? PriceGoodsVAT { get; set; }
    public string? PriceSum { get; set; }
    public string? PriceSell { get; set; }
    public string? PriceSellVAT { get; set; }
}
