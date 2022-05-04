using MongoDB.Driver;
using RouteManager.Domain.Core.Entities;
using RouteManager.Domain.Core.Repository;
using Teams.Domain.Contracts.v1;
using Teams.Domain.Entities.v1;

namespace Teams.Infra.Data.Repositories.v1
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
