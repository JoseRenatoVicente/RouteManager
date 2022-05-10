using FluentValidation;
using FluentValidation.Results;
using MediatR;
using RouteManager.Domain.Core.Contracts;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RouteManager.Domain.Core.Pipelines;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IBaseCommand
{
    private readonly INotifier _notifier;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(INotifier notifier, IEnumerable<IValidator<TRequest>> validators)
    {
        _notifier = notifier;
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Any())
        {
            Notification(failures);
            return default;
        }
        return await next();
    }

    private void Notification(IEnumerable<ValidationFailure> failures)
    {
        foreach (var error in failures)
            _notifier.Handle(error.ErrorMessage);
    }


}