using System.Text.Json.Serialization;

namespace ApiServices.DTOs.Products;

public class Product
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("categoryId")]
    public long CategoryId { get; set; }
    
    [JsonPropertyName("measureId")]
    public long MeasureId { get; set; }
    
    [JsonPropertyName("quantity")]
    public decimal Quantity { get; set; }
    
    [JsonPropertyName("minStock")]
    public int MinStock { get; set; }
    
    [JsonPropertyName("maxStock")]
    public int MaxStock { get; set; }

    [JsonPropertyName("createdBy")]
    public long CreatedBy { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("updatedBy")]
    public long UpdatedBy { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTimeOffset UpdatedAt { get; set; }

}
