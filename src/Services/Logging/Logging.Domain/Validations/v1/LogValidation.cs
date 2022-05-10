using FluentValidation;
using Logging.Domain.Entities.v1;

namespace Logging.Domain.Validations.v1;

public class LogValidation : AbstractValidator<Log>
{
    public LogValidation()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
    }
}