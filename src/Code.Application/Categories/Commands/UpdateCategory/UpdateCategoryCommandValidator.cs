using Code.Application.Common.Interfaces;
using FluentValidation;

namespace Code.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    private readonly IApplicationDbContext _dbContext;
    public UpdateCategoryCommandValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;


        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Category is required")
            .MaximumLength(200).WithMessage("Categiry/Name must not exceed 200 characters")
            .MustAsync(BeUniqName).WithMessage("The specified Category/Name already exsist");
    }
    public async Task<bool> BeUniqName(UpdateCategoryCommand model, string name, CancellationToken cancellationToken)
    {
        return await _dbContext.Categories
            .Where(c => c.Id != model.Id)
            .AllAsync(c => c.Name != name, cancellationToken);
    }
}
