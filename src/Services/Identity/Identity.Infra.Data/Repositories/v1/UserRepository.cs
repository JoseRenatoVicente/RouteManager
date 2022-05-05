using Identity.Domain.Contracts.v1;
using Identity.Domain.Entities.v1;
using MongoDB.Driver;
using RouteManager.Domain.Core.Repository;

namespace Identity.Infra.Data.Repositories.v1;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IMongoDatabase database) : base(database)
    {
    }
}