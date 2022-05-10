using MediatR;
using RouteManager.WebAPI.Core.Notifications;

namespace RouteManager.Domain.Core.Handlers;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Response> 
    where TCommand : IRequest<Response>
{
}

