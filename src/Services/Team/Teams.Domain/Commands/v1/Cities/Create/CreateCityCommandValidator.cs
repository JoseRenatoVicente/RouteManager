using FluentValidation;

namespace Teams.Domain.Commands.v1.Cities.Create;

public sealed class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
{
    public CreateCityCommandValidator()
    {

        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("O campo Nome precisa ser fornecido")
            .Length(3, 60).WithMessage("O campo Nome precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(command => command.State)
            .NotEmpty().WithMessage("O campo Estado precisa ser fornecido")
            .Length(2).WithMessage("O campo Estado precisa ter entre {MinLength} e {MaxLength} caracteres");
    }
}