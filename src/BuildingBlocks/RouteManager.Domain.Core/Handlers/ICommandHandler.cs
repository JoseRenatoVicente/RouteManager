using MediatR;
using RouteManager.Domain.Core.Contracts;
using RouteManager.WebAPI.Core.Notifications;

namespace RouteManager.Domain.Core.Handlers;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Response>
    where TCommand : IBaseCommand
{
}

