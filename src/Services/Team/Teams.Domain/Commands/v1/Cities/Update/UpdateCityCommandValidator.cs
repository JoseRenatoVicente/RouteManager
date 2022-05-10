using FluentValidation;

namespace Teams.Domain.Commands.v1.Cities.Update;

public sealed class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
{
    public UpdateCityCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("O campo Id precisa ser fornecido")
            .Length(24).WithMessage("O campo Id precisa ter 24 caracteres");

        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("O campo Nome precisa ser fornecido")
            .Length(3, 60).WithMessage("O campo Nome precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(command => command.State)
            .NotEmpty().WithMessage("O campo Estado precisa ser fornecido")
            .Length(2).WithMessage("O campo Estado precisa ter entre {MinLength} e {MaxLength} caracteres");
    }
}