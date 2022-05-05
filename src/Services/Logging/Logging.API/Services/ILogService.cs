using System.Collections.Generic;
using System.Threading.Tasks;
using Logging.Domain.Entities.v1;

namespace Logging.API.Services;

public interface ILogService
{
    Task<Log> GetLogByIdAsync(string id);
    Task<IEnumerable<Log>> GetLogsAsync();
    Task<IEnumerable<Log>> GetLogsByEntityIdAsync(string entityId);
}