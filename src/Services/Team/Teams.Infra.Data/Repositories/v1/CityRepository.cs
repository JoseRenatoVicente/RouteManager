using MongoDB.Driver;
using RouteManager.Domain.Core.Repository;
using Teams.Domain.Contracts.v1;
using Teams.Domain.Entities.v1;

namespace Teams.Infra.Data.Repositories.v1;

public class CityRepository : BaseRepository<City>, ICityRepository
{
    public CityRepository(IMongoDatabase database) : base(database)
    {
    }
}