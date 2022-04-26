using MongoDB.Driver;
using RouteManager.Domain.Entities;
using RouteManager.Domain.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Routes.API.Repository
{
    public class RouteRepository : BaseRepository<Route>, IRouteRepository
    {
        public RouteRepository(IMongoDatabase database) : base(database)
        {
        }

    }
}
