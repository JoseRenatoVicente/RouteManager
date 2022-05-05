using FluentValidation;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Validations.v1;

public class CityValidation : AbstractValidator<City>
{
    public CityValidation()
    {

        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("O campo Nome precisa ser fornecido")
            .Length(3, 60).WithMessage("O campo Nome precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(command => command.State)
            .NotEmpty().WithMessage("O campo Estado precisa ser fornecido")
            .Length(2).WithMessage("O campo Estado precisa ter entre {MinLength} e {MaxLength} caracteres");

    }
}