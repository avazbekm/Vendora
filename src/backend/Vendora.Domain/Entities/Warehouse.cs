namespace Vendora.Domain.Entities;

using Vendora.Domain.Commons;

public class Warehouse : Auditable
{
    public string Name { get; set; } = string.Empty;
    public long UserId { get; set; }
    public string Address { get; set; } = string.Empty;
}
