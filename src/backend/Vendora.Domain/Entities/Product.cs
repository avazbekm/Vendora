﻿namespace Vendora.Domain.Entities;

using Vendora.Domain.Commons;

public class Product : Auditable
{
    public string Name { get; set; } = string.Empty;
    public long CategoryId { get; set; }
    public long MeasureId { get; set; }
    public decimal MinStock { get; set; }
    public decimal MaxStock { get; set; }
}
