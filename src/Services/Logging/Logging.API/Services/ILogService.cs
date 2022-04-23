using RouteManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndreAirLines.Domain.Services
{
    public interface ILogService
    {
        Task<Log> AddLogAsync(Log log);
        Task<Log> GetLogByIdAsync(string id);
        Task<IEnumerable<Log>> GetLogsAsync();
        Task RemoveLogAsync(Log log);
        Task RemoveLogAsync(string id);
        Task<Log> UpdateLogAsync(Log log);
    }
}