using MongoDB.Driver;
using RouteManager.Domain.Core.Repository;
using Routes.Domain.Contracts.v1;
using Routes.Domain.Entities.v1;

namespace Routes.Infra.Data.Repositories.v1;

public class ExcelFileRepository : BaseRepository<ExcelFile>, IExcelFileRepository
{
    public ExcelFileRepository(IMongoDatabase database) : base(database)
    {
    }
}