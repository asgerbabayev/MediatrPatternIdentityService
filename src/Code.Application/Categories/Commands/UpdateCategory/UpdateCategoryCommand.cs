using AutoMapper;
using Code.Application.Common.Exceptions;
using Code.Application.Common.Interfaces;
using Code.Application.Common.Mapping;
using MediatR;

namespace Code.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest, IMapFrom<Category>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public UpdateCategoryCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await _dbContext.Categories.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
        if (category is null)
            throw new NotFoundException("UpdateCategoryCommand"); 

        //category.Description = request.Description;
        //category.Name = request.Name;
        //Asp boilerplate
        _dbContext.Categories.Update(_mapper.Map<Category>(request));
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
