using Logging.Domain.Contracts.v1;
using Logging.Domain.Validations.v1;
using RouteManager.Domain.Core.Services.Base;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndreAirLines.Domain.Services
{
    public class LogService : BaseService, ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository, INotifier notifier) : base(notifier)
        {
            _logRepository = logRepository;
        }

        public async Task<IEnumerable<Log>> GetLogsAsync() =>
            await _logRepository.GetAllAsync();

        public async Task<Log> GetLogByIdAsync(string id) =>
            await _logRepository.FindAsync(c => c.Id == id);

        public async Task<IEnumerable<Log>> GetLogsByEntityIdAsync(string entityId) =>
            await _logRepository.FindAllAsync(c => c.EntityId == entityId);

        public async Task<Log> AddLogAsync(Log log)
        {
            return !ExecuteValidation(new LogValidation(), log) ? log : await _logRepository.AddAsync(log);
        }

        public async Task<Log> UpdateLogAsync(Log log)
        {
            return !ExecuteValidation(new LogValidation(), log) ? log : await _logRepository.UpdateAsync(log);
        }

        public async Task RemoveLogAsync(Log log) =>
            await _logRepository.RemoveAsync(log);

        public async Task RemoveLogAsync(string id) =>
            await _logRepository.RemoveAsync(id);

    }
}
