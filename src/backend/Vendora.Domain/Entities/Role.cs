using Vendora.Domain.Commons;

namespace Vendora.Domain.Entities;

public class Role : Auditable
{
    public string RoleName { get; set; } = string.Empty;
    public long UserId { get; set; }
}
