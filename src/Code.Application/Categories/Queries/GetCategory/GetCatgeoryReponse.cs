using Code.Application.Common.Mapping;
using Code.Domain.Entities;

namespace Code.Application.Categories.Queries.GetCategory;

public class GetCatgeoryReponse:IMapFrom<Category>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
