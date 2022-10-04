using Code.Application.Common.Interfaces;
using FluentValidation;

namespace Code.Application.Products.Commands.CreateProduct;
public class CreateProductValidator : AbstractValidator<Product>
{

    public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Category is required")
            .MaximumLength(128).WithMessage("Product/Name must not exceed 128 characters");
    }

}
