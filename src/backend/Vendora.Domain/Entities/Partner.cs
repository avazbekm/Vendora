namespace Vendora.Domain.Entities;

using Vendora.Domain.Commons;

public class Partner : Auditable
{
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
