using MediatR;

namespace Code.Application.Categories.Commands.CreateCategory;

public class CreateCategoryComand : IRequest<int>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
