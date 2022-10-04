namespace Code.Domain.Entities;

public class Category : BaseAuditableEntity
{
    public Category()
    {
        Products = new HashSet<Product>();
    }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}
