using Code.Application.Common.Mapping;
using MediatR;

namespace Code.Application.Categories.Commands.CreateCategory;

public class CreateCategoryComand : IRequest<int>, IMapFrom<Category>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
