using Code.Application.Common.Interfaces;
using FluentValidation;

namespace Code.Application.Categories.Commands.CreateCategory;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryComand>
{
    private readonly IApplicationDbContext _dbContext;
    public CreateCategoryValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Category is required")
            .MaximumLength(200).WithMessage("Category/Name must not exceed 200 characters")
            .MustAsync(BeUniqName).WithMessage("The specified Category/Name already exsist");
    }
    public async Task<bool> BeUniqName(string name, CancellationToken cancellationToken)
    {
        return await _dbContext.Categories.AllAsync(c => c.Name != name, cancellationToken);
    }
}
