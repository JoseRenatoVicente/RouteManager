using MongoDB.Driver;
using RouteManager.Domain.Entities;
using RouteManager.Domain.Repository;

namespace Routes.API.Repository
{
    public class ExcelFileRepository : BaseRepository<ExcelFile>, IExcelFileRepository
    {
        public ExcelFileRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
