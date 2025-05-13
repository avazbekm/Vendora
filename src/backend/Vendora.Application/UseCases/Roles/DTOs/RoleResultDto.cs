namespace Vendora.Application.UseCases.Roles.DTOs;

public class RoleResultDto
{
    public long Id { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public long CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public long? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
