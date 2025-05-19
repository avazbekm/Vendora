namespace Vendora.Domain.Entities;

using Vendora.Domain.Commons;

public class Measure : Auditable
{
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string? Barcode { get; set; } = string.Empty;
}
