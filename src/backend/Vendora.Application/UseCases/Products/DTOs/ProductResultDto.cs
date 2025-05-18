namespace Vendora.Application.UseCases.Products.DTOs;

public class ProductResultDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public long CategoryId { get; set; }
    public long MeasureId { get; set; }
    public decimal Quantity { get; set; }
    public int MinStock { get; set; }
    public int MaxStock { get; set; }
    public long CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public long UpdatedBy { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
