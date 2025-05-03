namespace Vendora.Domain.Commons;

public class Auditable
{
    public long Id { get; set; }
    public long CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public long UpdatedBy { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
