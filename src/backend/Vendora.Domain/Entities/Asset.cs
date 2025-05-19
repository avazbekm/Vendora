namespace Vendora.Domain.Entities;

using Vendora.Domain.Commons;

public class Asset : Auditable
{
    public string FilePath { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
}
