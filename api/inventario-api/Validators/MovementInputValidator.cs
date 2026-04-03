using FluentValidation;
using inventario_api.DTOs;

namespace inventario_api.Validators
{
    public class MovementInputValidator : AbstractValidator<MovementInput>
    {
        public MovementInputValidator()
        {
            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("Tipo de movimentação inválido.");


            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");

        }
    }
}
