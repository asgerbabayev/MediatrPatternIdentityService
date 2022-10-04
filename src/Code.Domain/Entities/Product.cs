namespace Code.Domain.Entities;

public class Product : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public short UnitsInStock { get; set; }
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
}
