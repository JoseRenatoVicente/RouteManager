using FluentValidation;
using RouteManager.Domain.Core.Entities;

namespace Logging.Domain.Validations.v1
{
    public class LogValidation : AbstractValidator<Log>
    {
        public LogValidation()
        {

        }
    }
}
