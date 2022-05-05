using Logging.Domain.Contracts.v1;
using Logging.Domain.Entities.v1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logging.API.Services;

public class LogService : ILogService
{
    private readonly ILogRepository _logRepository;

    public LogService(ILogRepository logRepository)
    {
        _logRepository = logRepository;
    }

    public async Task<IEnumerable<Log>> GetLogsAsync() =>
        await _logRepository.GetAllAsync();

    public async Task<Log> GetLogByIdAsync(string id) =>
        await _logRepository.FindAsync(c => c.Id == id);

    public async Task<IEnumerable<Log>> GetLogsByEntityIdAsync(string entityId) =>
        await _logRepository.FindAllAsync(c => c.EntityId == entityId);
    

}