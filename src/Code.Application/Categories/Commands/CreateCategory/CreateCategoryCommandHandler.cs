using Code.Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Code.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryComand, int>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateCategoryComand request, CancellationToken cancellationToken)
    {

        EntityEntry<Category> category = _dbContext.Categories.Add(_mapper.Map<Category>(request));
        await _dbContext.SaveChangesAsync(cancellationToken);
        return category.Entity.Id;
    }
}
