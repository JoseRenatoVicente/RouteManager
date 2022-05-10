using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using Teams.Domain.Contracts.v1;

namespace Teams.Domain.Commands.v1.Teams.Delete;

public sealed class DeleteTeamCommandHandler : ICommandHandler<DeleteTeamCommand>
{
    private readonly IPersonRepository _personRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly INotifier _notifier;

    public DeleteTeamCommandHandler(IPersonRepository personRepository, ITeamRepository teamRepository, INotifier notifier)
    {
        _personRepository = personRepository;
        _teamRepository = teamRepository;
        _notifier = notifier;
    }

    public async Task<Response> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.FindAsync(teamFilter => teamFilter.Id == request.Id);

        if (team == null)
        {
            _notifier.Handle("Equipe não encontrada");
            return new Response();
        }

        foreach (var person in team.People!)
        {
            person.Active = true;
            await _personRepository.UpdateAsync(person);
        }

        await _teamRepository.RemoveAsync(team);

        return new Response();
    }
}