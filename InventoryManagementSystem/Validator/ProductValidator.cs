using FluentValidation;
using InventoryManagementSystem.Api.DTO.Request;
namespace InventoryManagementSystem.API.Validator
{
    public class ProductValidator : AbstractValidator<ProductRequest>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Product name is required.")
           .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

            RuleFor(p => p.Category)
                .NotEmpty().WithMessage("Category is required.")
                .MaximumLength(50).WithMessage("Category must not exceed 50 characters.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(p => p.Quantity)
                .GreaterThan(0).WithMessage("Quantity cannot be negative.");
        }
    }
}
