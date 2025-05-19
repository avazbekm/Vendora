using Vendora.Domain.Commons;

namespace Vendora.Domain.Entities;

public class Category : Auditable
{
    public string Name { get; set; } = string.Empty;
    public bool Active { get; set; }
}
