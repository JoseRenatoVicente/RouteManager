using Routes.Domain.Contracts.v1;
using Routes.Domain.Entities.v1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Routes.API.Services;

public interface IExcelFileService
{
    Task<IEnumerable<ExcelFile>> GetExcelFilesAsync();
    Task<ExcelFile> GetExcelFileByIdAsync(string id);
}

public class ExcelFileService : IExcelFileService
{
    private readonly IExcelFileRepository _excelFileRepository;

    public ExcelFileService(IExcelFileRepository excelFileRepository)
    {
        _excelFileRepository = excelFileRepository;
    }

    public async Task<IEnumerable<ExcelFile>> GetExcelFilesAsync()
    {
        return await _excelFileRepository.GetAllAsync();
    }

    public async Task<ExcelFile> GetExcelFileByIdAsync(string id)
    {
        return await _excelFileRepository.FindAsync(excelFile => excelFile.Id == id);
    }
}