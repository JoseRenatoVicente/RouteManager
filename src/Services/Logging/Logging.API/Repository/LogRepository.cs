using MongoDB.Driver;
using RouteManager.Domain.Entities;
using RouteManager.Domain.Repository;

namespace Logs.API.Repository
{
    public class LogRepository : BaseRepository<Log>, ILogRepository
    {
        public LogRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
