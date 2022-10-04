using MediatR;

namespace Code.Application.Categories.Queries.GetCategories;
public record GetCategoriesQuery : IRequest<IEnumerable<GetCategoriesResponse>>;
