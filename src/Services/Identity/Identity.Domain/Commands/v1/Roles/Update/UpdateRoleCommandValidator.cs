using FluentValidation;

namespace Identity.Domain.Commands.v1.Roles.Update;

public sealed class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("O campo Id precisa ser fornecido")
            .Length(24).WithMessage("O campo Id precisa ter 24 caracteres");

        RuleFor(command => command.Description)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 60).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(command => command.Claims)
            .NotEmpty().WithMessage("Nenhuma claim selecionada");
    }
}