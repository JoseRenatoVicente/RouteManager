using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using Teams.Domain.Contracts.v1;

namespace Teams.Domain.Commands.v1.People.Delete;

public sealed class DeletePersonCommandHandler : ICommandHandler<DeletePersonCommand>
{
    private readonly IPersonRepository _personRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly INotifier _notifier;

    public DeletePersonCommandHandler(IPersonRepository personRepository, ITeamRepository teamRepository, INotifier notifier)
    {
        _personRepository = personRepository;
        _teamRepository = teamRepository;
        _notifier = notifier;
    }

    public async Task<Response> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.FindAsync(personFilter => personFilter.Id == request.Id);

        if (person == null)
        {
            _notifier.Handle("Pessoa não encontrada");
            return new Response();
        }

        if (await _teamRepository.FindAsync(c => c.People!.Any(personFilter => personFilter.Id == person.Id)) != null)
        {
            _notifier.Handle("Essa Pessoa possui vinculo com uma equipe, exclua primeiro a equipe para excluir a pessoa");
            return new Response();
        }

        await _personRepository.RemoveAsync(person);

        return new Response();
    }
}