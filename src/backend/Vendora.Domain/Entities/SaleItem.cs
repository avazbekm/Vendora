namespace Vendora.Domain.Entities;

using Vendora.Domain.Commons;

public class SaleItem : Auditable
{
    public long SaleId { get; set; }
    public long ProductId { get; set; }
    public long MeasureId { get; set; }
    public decimal CostPrice { get; set; }
    public decimal Benefit { get; set; }
    public decimal SalePrice { get; set; }
    public decimal? VatPercent { get; set; }
    public decimal PriceWithVat { get; set; }
    public decimal Quantity { get; set; }
    public decimal Amount { get; set; }
    public decimal? VatSum { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalBenefit { get; set; }
}
