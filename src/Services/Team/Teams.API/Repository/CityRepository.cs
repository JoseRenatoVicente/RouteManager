using MongoDB.Driver;
using RouteManager.Domain.Entities;
using RouteManager.Domain.Repository;

namespace Teams.API.Repository
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
