namespace Vendora.Domain.Entities;

using Vendora.Domain.Commons;

public class Transaction : Auditable
{
    public long PartnerId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public Status Status { get; set; }
    public long SaleId { get; set; }
}