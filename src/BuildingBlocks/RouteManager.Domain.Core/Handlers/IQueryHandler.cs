using MediatR;
using RouteManager.Domain.Core.Contracts;
using RouteManager.WebAPI.Core.Notifications;

namespace RouteManager.Domain.Core.Handlers;

public interface IQueryHandler<TQuery> : IRequestHandler<TQuery, Response>
    where TQuery : IBaseQuery
{
}