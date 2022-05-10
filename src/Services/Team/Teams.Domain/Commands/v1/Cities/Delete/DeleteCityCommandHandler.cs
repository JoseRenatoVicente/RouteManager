using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using Teams.Domain.Contracts.v1;

namespace Teams.Domain.Commands.v1.Cities.Delete;

public sealed class DeleteCityCommandHandler : ICommandHandler<DeleteCityCommand>
{
    private readonly ICityRepository _cityRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly INotifier _notifier;

    public DeleteCityCommandHandler(ICityRepository cityRepository, ITeamRepository teamRepository, INotifier notifier)
    {
        _cityRepository = cityRepository;
        _teamRepository = teamRepository;
        _notifier = notifier;
    }

    public async Task<Response> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.FindAsync(cityFilter => cityFilter.Id == request.Id);

        if (city == null)
        {
            _notifier.Handle("Cidade não encontrada");
            return new Response();
        }

        if (await _teamRepository.FindAsync(c => c.City!.Id == city.Id) != null)
        {
            _notifier.Handle("Essa cidade possui equipes vinculadas exclua primeiro a equipe para excluir a cidade");
            return new Response();
        }

        await _cityRepository.RemoveAsync(city);

        return new Response();
    }
}