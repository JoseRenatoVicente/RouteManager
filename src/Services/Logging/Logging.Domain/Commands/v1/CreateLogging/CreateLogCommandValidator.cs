using FluentValidation;

namespace Logging.Domain.Commands.v1.CreateLogging;

public sealed class CreateLogCommandValidator : AbstractValidator<CreateLogCommand>
{
    public CreateLogCommandValidator()
    {
        RuleFor(command => command.EntityId)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
    }
}