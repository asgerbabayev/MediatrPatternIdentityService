using MediatR;

namespace Code.Application.Categories.Queries.GetCategory;

public record GetCategoryQuery(int Id) : IRequest<GetCatgeoryReponse>;
