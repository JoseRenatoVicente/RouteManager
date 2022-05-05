using Identity.Domain.Commands.Roles.Delete;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using Routes.Domain.Contracts.v1;

namespace Routes.Domain.Commands.ExcelFiles.Delete;

public class DeleteExcelFileCommandHandler : CommandHandler<DeleteExcelFileCommand>
{
    private readonly IExcelFileRepository _excelFileRepository;

    public DeleteExcelFileCommandHandler(INotifier notifier, IExcelFileRepository excelFileRepository) : base(notifier)
    {
        _excelFileRepository = excelFileRepository;
    }

    public override async Task<Response> Handle(DeleteExcelFileCommand request, CancellationToken cancellationToken)
    {
        var excelFile = await _excelFileRepository.FindAsync(excelFileFilter=> excelFileFilter.Id == request.Id);
        if (excelFile == null)
        {
            Notification("Rota não encontrada");
            return new Response();
        }
        await _excelFileRepository.RemoveAsync(request.Id);

        return new Response();
    }
}