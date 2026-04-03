using FluentValidation;
using inventario_api.DTOs;

namespace inventario_api.Validators
{
    public class CategoryInputValidator : AbstractValidator<CategoryInput>
    {
        public CategoryInputValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome da categoria é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.")
                .MaximumLength(60).WithMessage("O nome deve ter no máximo 60 caracteres.");
        }
    }
}