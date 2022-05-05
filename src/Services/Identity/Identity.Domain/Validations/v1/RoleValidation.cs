using FluentValidation;
using Identity.Domain.Entities.v1;

namespace Identity.Domain.Validations.v1;

public class RoleValidation : AbstractValidator<Role>
{
    public RoleValidation()
    {
        RuleFor(command => command.Description)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 60).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(command => command.Description)
            .NotEmpty().WithMessage("Nenhuma claim selecionada");

    }
}