using Services.DTOs.Enums;

namespace Services.DTOs.Users;

public class User
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronomyc { get; set; } = string.Empty;

    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string PasportSeria { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public long RoleId { get; set; }
    public long? PhotoId { get; set; }
    public long CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public long UpdatedBy { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
