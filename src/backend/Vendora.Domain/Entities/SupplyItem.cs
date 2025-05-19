namespace Vendora.Domain.Entities;

using Vendora.Domain.Commons;

public class SupplyItem : Auditable
{
    public long SupplyId { get; set; }
    public long ProductId { get; set; }
    public decimal Quantity { get; set; }
    public long MeasureId { get; set; }
    public decimal CostPrice { get; set; }
    public decimal Amount { get; set; }
    public decimal? VatPercent { get; set; }
    public decimal? VatSum { get; set; }
    public decimal Total { get; set; }
}
