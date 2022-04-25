using MongoDB.Driver;
using RouteManager.Domain.Entities;
using RouteManager.Domain.Repository;

namespace Teams.API.Repository
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
