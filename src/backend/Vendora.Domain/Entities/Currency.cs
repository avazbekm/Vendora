namespace Vendora.Domain.Entities;

using Vendora.Domain.Commons;

public class Currency : Auditable
{
    public string Name { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public decimal KursIn { get; set; }
    public decimal KursOut { get; set; }
}
