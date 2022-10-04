using AutoMapper;
using Code.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace Code.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
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
            throw new Exception(""); //Todo: Custom Exception 

        //category.Description = request.Description;
        //category.Name = request.Name;
        //Asp boilerplate
        _dbContext.Categories.Update(_mapper.Map<Category>(request));
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
