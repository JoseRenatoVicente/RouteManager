using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using Routes.Domain.Contracts.v1;

namespace Routes.Domain.Commands.v1.ExcelFiles.Delete;

public sealed class DeleteExcelFileCommandHandler : ICommandHandler<DeleteExcelFileCommand>
{
    private readonly IExcelFileRepository _excelFileRepository;
    private readonly INotifier _notifier;

    public DeleteExcelFileCommandHandler(IExcelFileRepository excelFileRepository, INotifier notifier)
    {
        _excelFileRepository = excelFileRepository;
        _notifier = notifier;
    }

    public async Task<Response> Handle(DeleteExcelFileCommand request, CancellationToken cancellationToken)
    {
        var excelFile = await _excelFileRepository.FindAsync(excelFileFilter => excelFileFilter.Id == request.Id);
        if (excelFile == null)
        {
            _notifier.Handle("Rota não encontrada");
            return new Response();
        }
        await _excelFileRepository.RemoveAsync(request.Id);

        return new Response();
    }
}