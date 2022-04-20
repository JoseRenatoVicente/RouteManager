using MongoDB.Driver;
using RouteManager.Domain.Entities.Identity;
using RouteManager.Domain.Repository;

namespace Identity.API.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
