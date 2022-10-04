using AutoMapper;
using Code.Application.Categories.Commands.CreateCategory;
using Code.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Code.Application.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(int Id) : IRequest;

public class CreateCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateCategoryCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
    }


    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await _dbContext.Categories.FindAsync(request.Id);
        if (category is null)
            throw new Exception();
        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

}
