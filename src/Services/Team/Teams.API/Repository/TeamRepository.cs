using MongoDB.Driver;
using RouteManager.Domain.Entities;
using RouteManager.Domain.Repository;

namespace Teams.API.Repository
{
    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        public TeamRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
