using AutoMapper;
using AutoMapper.QueryableExtensions;
using Code.Application.Categories.Queries.GetCategories;
using Code.Application.Common.Interfaces;
using MediatR;

namespace Code.Application.Categories.Queries.GetCategory;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, GetCatgeoryReponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCategoryQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GetCatgeoryReponse> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<GetCatgeoryReponse>(await _dbContext.Categories.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken));
    }
}
