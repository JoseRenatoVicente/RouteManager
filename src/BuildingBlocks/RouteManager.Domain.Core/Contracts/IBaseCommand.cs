using MediatR;
using RouteManager.WebAPI.Core.Notifications;

namespace RouteManager.Domain.Core.Contracts;

public interface IBaseCommand : IRequest<Response>
{
}
