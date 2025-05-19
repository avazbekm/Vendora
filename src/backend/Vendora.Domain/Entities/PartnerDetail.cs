namespace Vendora.Domain.Entities;

using Vendora.Domain.Commons;

public class PartnerDetail : Auditable
{
    public long PartnerId { get; set; }
    public string? CompanyName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public string? BankAccount { get; set; } = string.Empty;
    public string? BankCode { get; set; } = string.Empty;
    public string? TaxInfo { get; set; } = string.Empty;
    public string? Chiefman { get; set; } = string.Empty;
}
