namespace Vendora.Domain.Entities;

using Vendora.Domain.Commons;

public class Stocks : Auditable
{
    public long WarehouseId { get; set; }
    public long ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal CostPrice { get; set; }
    public decimal SalePrice { get; set; }
    public long MeasureId { get; set; }
    public long CurrencyId { get; set; }
}
