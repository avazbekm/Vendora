namespace Vendora.Domain.Entities;

using Vendora.Domain.Commons;

public class ProductPhoto : Auditable
{
    public long ProductId { get; set; }
    public long AssetId { get; set; }
}
