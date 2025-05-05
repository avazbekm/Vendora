namespace Vendora.Domain.Entities;

using Vendora.Domain.Commons;

public class User : Auditable
{
    public string FirstName { get; set; } = string.Empty;
    public string? Login { get; set; }
    public string? LastName { get; set; }
    public string Password { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    public long PhotoId { get; set; }
}
