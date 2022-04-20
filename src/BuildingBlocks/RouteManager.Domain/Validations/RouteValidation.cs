using FluentValidation;
using RouteManager.Domain.Entities;

namespace RouteManager.Domain.Validations
{
    public class RouteValidation : AbstractValidator<Route>
    {
        public RouteValidation()
        {
            RuleFor(command => command.OS)
             .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
             .Length(1, 11).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(command => command.Base)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(command => command.Service)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(3, 60).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(command => command.Address).SetValidator(new AddressValidation());
        }
    }
}
