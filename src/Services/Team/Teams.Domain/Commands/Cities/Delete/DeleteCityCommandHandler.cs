using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using Teams.Domain.Contracts.v1;

namespace Teams.Domain.Commands.Cities.Delete;

public class DeleteCityCommandHandler : CommandHandler<DeleteCityCommand>
{
    private readonly ICityRepository _cityRepository;
    private readonly ITeamRepository _teamRepository;

    public DeleteCityCommandHandler(ICityRepository cityRepository, ITeamRepository teamRepository, INotifier notifier) : base(notifier)
    {
        _cityRepository = cityRepository;
        _teamRepository = teamRepository;
    }

    public override async Task<Response> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.FindAsync(cityFilter => cityFilter.Id == request.Id);

        if (city == null)
        {
            Notification("Cidade não encontrada");
            return new Response();
        }

        if (await _teamRepository.FindAsync(c => c.City!.Id == city.Id) != null)
        {
            Notification("Essa cidade possui equipes vinculadas exclua primeiro a equipe para excluir a cidade");
            return new Response();
        }

        await _cityRepository.RemoveAsync(city);

        return new Response();
    }
}