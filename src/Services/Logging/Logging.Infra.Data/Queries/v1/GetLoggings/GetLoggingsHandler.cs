using Logging.Domain.Contracts.v1;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;

namespace Logging.Infra.Data.Queries.v1.GetLoggings
{
    public class GetLoggingsHandler : IQueryHandler<GetLoggingsQuery>
    {
        private readonly ILogRepository _logRepository;

        public GetLoggingsHandler(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<Response> Handle(GetLoggingsQuery query,
            CancellationToken cancellationToken)
        {
            return new Response { Content = await _logRepository.GetAllAsync() };
        }
    }
}
