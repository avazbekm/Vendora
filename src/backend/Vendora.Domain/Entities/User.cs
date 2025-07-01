namespace Vendora.Domain.Entities;

using Vendora.Domain.Enums;
using Vendora.Domain.Commons;

public class User : Auditable
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Patronomyc { get; set; } = string.Empty;

    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string? PasportSeria {  get; set; } = string.Empty;
    public DateTimeOffset? DateOfIssue { get; set; }  // pasport berilgan sana
    public DateTimeOffset? DateOfExpiry { get; set; } // Amal qilish muddati

    public string Phone { get; set; } = string.Empty;   
    public DateTimeOffset? DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public long RoleId { get; set; }
    public string? Address {  get; set; }
    public string? JShShIR {  get; set; }
    public long? PhotoId { get; set; }
}
