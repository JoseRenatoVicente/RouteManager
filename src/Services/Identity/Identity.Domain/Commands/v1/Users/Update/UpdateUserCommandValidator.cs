using FluentValidation;

namespace Identity.Domain.Commands.v1.Users.Update;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("O campo Id precisa ser fornecido")
            .Length(24).WithMessage("O campo Id precisa ter 24 caracteres");

        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 60).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(command => command.Email)
            .NotEmpty().WithMessage("O campo Email deve ser fornecido")
            .EmailAddress().WithMessage("Digite um Email válido");

        RuleFor(command => command.UserName)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
    }
}