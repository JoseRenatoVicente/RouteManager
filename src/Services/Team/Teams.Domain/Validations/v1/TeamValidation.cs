using FluentValidation;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Validations.v1;

public class TeamValidation : AbstractValidator<Team>
{
    public TeamValidation()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 60).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.City!).SetValidator(new CityValidation());
    }
}