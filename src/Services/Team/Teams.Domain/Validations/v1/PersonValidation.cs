using FluentValidation;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Validations.v1;

public class PersonValidation : AbstractValidator<Person>
{
    public PersonValidation()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 60).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

    }
}