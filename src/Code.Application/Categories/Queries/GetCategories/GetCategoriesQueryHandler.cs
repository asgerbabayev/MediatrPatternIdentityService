using AutoMapper;
using AutoMapper.QueryableExtensions;
using Code.Application.Common.Interfaces;
using MediatR;

namespace Code.Application.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<GetCategoriesResponse>>
{
    private IApplicationDbContext _dbContext;
    private IMapper _mapper;

    public GetCategoriesQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetCategoriesResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Categories.AsNoTracking()
            .ProjectTo<GetCategoriesResponse>(_mapper.ConfigurationProvider)
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}