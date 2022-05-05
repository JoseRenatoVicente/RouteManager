using FluentValidation;
using FluentValidation.Results;
using MediatR;
using RouteManager.WebAPI.Core.Notifications;
using System.Threading;
using System.Threading.Tasks;

namespace RouteManager.Domain.Core.Handlers;

public abstract class CommandHandler<TCommand> : IRequestHandler<TCommand, Response> where TCommand : IRequest<Response>
{
    private readonly INotifier _notifier;

    protected CommandHandler(INotifier notifier)
    {
        _notifier = notifier;
    }

    private void Notification(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Notification(error.ErrorMessage);
        }
    }

    protected void Notification(string message)
    {
        _notifier.Handle(message);
    }

    protected bool ExecuteValidation<TValidation, TEntity>(TValidation validation, TEntity entity) where TValidation : AbstractValidator<TEntity>
    {
        var validator = validation.Validate(entity);

        if (validator.IsValid) return true;

        Notification(validator);

        return false;
    }

    public abstract Task<Response> Handle(TCommand request, CancellationToken cancellationToken);
}

