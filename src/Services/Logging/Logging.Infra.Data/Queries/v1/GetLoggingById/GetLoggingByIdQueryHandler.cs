using System.Text.Json;
using AutoMapper;
using Logging.Domain.Contracts.v1;
using Microsoft.Extensions.Caching.Distributed;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;

namespace Logging.Infra.Data.Queries.v1.GetLoggingById;

public class GetLoggingByIdQueryHandler : IQueryHandler<GetLoggingByIdQuery>
{
    private readonly ILogRepository _logRepository;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _distributedCache;

    public GetLoggingByIdQueryHandler(ILogRepository logRepository, IMapper mapper, IDistributedCache distributedCache)
    {
        _logRepository = logRepository;
        _mapper = mapper;
        _distributedCache = distributedCache;
    }

    public async Task<Response> Handle(GetLoggingByIdQuery query, CancellationToken cancellationToken)
    {
        var log = await GetSingleOrDefaultAsync(query, cancellationToken);

        if (log == null) return new Response();

        return new Response
        {
            Content = log
        };
    }


    private async Task<GetLoggingByIdQueryResponse?> GetSingleOrDefaultAsync(GetLoggingByIdQuery query,
        CancellationToken cancellationToken)
    {
        var logCached = await _distributedCache.GetStringAsync(GetCacheKey(query.Id), cancellationToken);

        if (!string.IsNullOrEmpty(logCached)) 
            return JsonSerializer.Deserialize<GetLoggingByIdQueryResponse>(logCached);

        var log = _mapper.Map<GetLoggingByIdQueryResponse>(await _logRepository.FindAsync(log => log.Id == query.Id));

        await SetCustomerCacheAsync(log, cancellationToken);

        return log;
    }

    private static string GetCacheKey(string logId)
    {
        return $"log_{logId}";
    }

    private async Task SetCustomerCacheAsync(GetLoggingByIdQueryResponse log, CancellationToken cancellationToken)
    {
        await _distributedCache.SetStringAsync(GetCacheKey(log.Id!), JsonSerializer.Serialize(log),
            cancellationToken);
    }
}