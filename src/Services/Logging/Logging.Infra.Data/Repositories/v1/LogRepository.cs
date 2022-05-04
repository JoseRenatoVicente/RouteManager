using Logging.Domain.Contracts.v1;
using MongoDB.Driver;
using RouteManager.Domain.Core.Entities;
using RouteManager.Domain.Core.Repository;

namespace Logging.Infra.Data.Repositories.v1
{
    public class LogRepository : BaseRepository<Log>, ILogRepository
    {
        public LogRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
