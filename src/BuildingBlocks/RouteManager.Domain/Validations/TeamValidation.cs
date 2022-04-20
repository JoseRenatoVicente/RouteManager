using FluentValidation;
using RouteManager.Domain.Entities;

namespace RouteManager.Domain.Validations
{
    public class TeamValidation : AbstractValidator<Team>
    {
        public TeamValidation()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 60).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.City).SetValidator(new CityValidation());
        }
    }
}
