using MongoDB.Driver;
using RouteManager.Domain.Entities.Identity;
using RouteManager.Domain.Repository;

namespace Identity.API.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
