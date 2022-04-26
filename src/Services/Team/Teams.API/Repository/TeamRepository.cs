using MongoDB.Driver;
using RouteManager.Domain.Entities;
using RouteManager.Domain.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Teams.API.Repository
{
    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        public TeamRepository(IMongoDatabase database) : base(database)
        {
        }

        public override async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await DbSet.Find(entity => true).SortBy(c=>c.City.Name).ToListAsync();
        }
    }
}
