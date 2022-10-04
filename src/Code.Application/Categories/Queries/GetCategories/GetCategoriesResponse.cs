using Code.Application.Common.Mapping;

namespace Code.Application.Categories.Queries.GetCategories;

public class GetCategoriesResponse : IMapFrom<Category>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
