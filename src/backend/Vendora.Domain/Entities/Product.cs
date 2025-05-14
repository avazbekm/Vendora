using Vendora.Domain.Commons;

namespace Vendora.Domain.Entities;

public class Product : Auditable
{
    public string Name { get; set; } = string.Empty;
    public long CategoryId { get; set; }
    public long MeasureId { get; set; }
    public decimal Quantity { get; set; }
    public int MinStock { get; set; }
    public int MaxStock { get; set; }
}
