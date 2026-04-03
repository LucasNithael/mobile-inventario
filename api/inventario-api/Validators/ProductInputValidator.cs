using FluentValidation;
using inventario_api.Domain.Shared;
using inventario_api.DTOs;

namespace inventario_api.Validators
{
    public class ProductInputValidator : AbstractValidator<ProductInput>
    {
        public ProductInputValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome da produto é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.")
                .MaximumLength(60).WithMessage("O nome deve ter no máximo 60 caracteres.");

        }
    }
}