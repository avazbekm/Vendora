using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendora.WPF.Models;

namespace Vendora.WPF.ViewModels
{
    public class SuppliesViewModel: ViewModelBase
    {
        private decimal? _totalSum;
        public DateTime DateSupply { get; set; } = DateTime.Now;
        public string DocNumber { get; set; }
        public string SupplierName { get; set; }
        public string CurrencyType { get; set; }
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
        public string Info { get; set; }
        public string TypeGoods { get; set; }
        public string NameGoods { get; set; }
        public string? AmountGoods { get; set; }
        public string UnitGoods { get; set; }
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
        public string TypeGoods { get; set; }
        public string NameGoods { get; set; }
        public string? AmountGoods { get; set; }
        public string UnitGoods { get; set; }
        public string? PriceGoods { get; set; }
        public decimal? RateVAT { get; set; }
        public decimal? PriceGoodsVAT { get; set; }
        public string? PriceSum { get; set; }
        public string? PriceSell { get; set; }
        public string? PriceSellVAT { get; set; }
    }
}
