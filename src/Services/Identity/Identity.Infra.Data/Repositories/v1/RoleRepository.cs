using Identity.Domain.Contracts.v1;
using Identity.Domain.Entities.v1;
using MongoDB.Driver;
using RouteManager.Domain.Core.Repository;

namespace Identity.Infra.Data.Repositories.v1;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(IMongoDatabase database) : base(database)
    {
    }
}