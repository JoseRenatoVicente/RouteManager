using FluentValidation;
using Logging.Domain.Entities.v1;

namespace Logging.Domain.Validations.v1;

public class LogValidation : AbstractValidator<Log>
{
    public LogValidation()
    {

    }
}