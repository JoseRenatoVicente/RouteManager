using FluentValidation;
using Identity.Domain.Entities.v1;

namespace Identity.Domain.Validations.v1
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {

            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 60).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(command => command.Email)
                .NotEmpty().WithMessage("O campo Email deve ser fornecido")
                .EmailAddress().WithMessage("Digite um Email válido");

            RuleFor(command => command.Password)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(8, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(command => command.Role).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        }
    }
}
