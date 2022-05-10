using AutoMapper;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using Teams.Domain.Contracts.v1;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.v1.People.Update;

public sealed class UpdatePersonCommandHandler : ICommandHandler<UpdatePersonCommand>
{
    private readonly ITeamRepository _teamRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly INotifier _notifier;

    public UpdatePersonCommandHandler(ITeamRepository teamRepository, IPersonRepository personRepository, IMapper mapper, INotifier notifier)
    {
        _teamRepository = teamRepository;
        _personRepository = personRepository;
        _mapper = mapper;
        _notifier = notifier;
    }

    public async Task<Response> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = _mapper.Map<Person>(request);

        var personBefore = await _personRepository.FindAsync(personFilter => personFilter.Id == person.Id);
        if (personBefore == null)
        {
            _notifier.Handle("Pessoa não encontrada");
            return new Response();
        }
        person.Active = personBefore.Active;

        var team = await _teamRepository.FindAsync(c => c.People!.Any(personFilter => personFilter.Id == person.Id));
        if (team != null)
        {
            team.People = team.People!.Select(c =>
            {
                if (c.Id == person.Id)
                {
                    c = person;
                }
                return c;
            });
            await _teamRepository.UpdateAsync(team);
        }

        await _personRepository.UpdateAsync(person);
        return new Response { Content = person };
    }
}