using MongoDB.Driver;
using RouteManager.Domain.Core.Repository;
using Teams.Domain.Contracts.v1;
using Teams.Domain.Entities.v1;

namespace Teams.Infra.Data.Repositories.v1;

public class TeamRepository : BaseRepository<Team>, ITeamRepository
{
    public TeamRepository(IMongoDatabase database) : base(database)
    {
    }

    public override async Task<IEnumerable<Team>> GetAllAsync()
    {
        return await DbSet.Find(entity => true).SortBy(c => c.City!.Name).ToListAsync();
    }
}