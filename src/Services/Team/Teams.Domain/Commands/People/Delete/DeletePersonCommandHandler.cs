using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using Teams.Domain.Contracts.v1;

namespace Teams.Domain.Commands.People.Delete;

public class DeletePersonCommandHandler : CommandHandler<DeletePersonCommand>
{
    private readonly IPersonRepository _personRepository;
    private readonly ITeamRepository _teamRepository;

    public DeletePersonCommandHandler(IPersonRepository personRepository, ITeamRepository teamRepository, INotifier notifier) : base(notifier)
    {
        _personRepository = personRepository;
        _teamRepository = teamRepository;
    }

    public override async Task<Response> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.FindAsync(personFilter => personFilter.Id == request.Id);

        if (person == null)
        {
            Notification("Pessoa não encontrada");
            return new Response();
        }

        if (await _teamRepository.FindAsync(c => c.People!.Any(personFilter => personFilter.Id == person.Id)) != null)
        {
            Notification("Essa Pessoa possui vinculo com uma equipe, exclua primeiro a equipe para excluir a pessoa");
            return new Response();
        }

        await _personRepository.RemoveAsync(person);

        return new Response();
    }
}