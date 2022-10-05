using Code.Application.Common.Exceptions;
using Code.Application.Common.Interfaces;
using MediatR;

namespace Code.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteCategoryCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await _dbContext.Categories.FindAsync(request.Id);
        if (category is null)
            throw new NotFoundException("DeleteCategoryCommand");
        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

}
