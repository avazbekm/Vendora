namespace Vendora.Domain.Entities;

using Vendora.Domain.Commons;
using Vendora.Domain.Enums;

public class User : Auditable
{
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string? Patronomyc { get; set; } = string.Empty;

    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string? PasportSeria { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTimeOffset? DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public long RoleId { get; set; }
    public long? PhotoId { get; set; }
}
