namespace Vendora.Domain.Entities;

using Vendora.Domain.Commons;

public class Sale : Auditable
{
    public long PartnerId { get; set; }
    public DateTimeOffset Date { get; set; }
    public long UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal? VatAmount { get; set; }
    public decimal TotalAmountWithVat { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public string DocumentDate { get; set; } = string.Empty;
    public long CurrencyId { get; set; }
    public decimal Kurs { get; set; }
}
