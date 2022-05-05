using AutoMapper;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using Teams.Domain.Contracts.v1;
using Teams.Domain.Entities.v1;
using Teams.Domain.Validations.v1;

namespace Teams.Domain.Commands.People.Update;

public class UpdatePersonCommandHandler : CommandHandler<UpdatePersonCommand>
{
    private readonly ITeamRepository _teamRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public UpdatePersonCommandHandler(IPersonRepository personRepository, ITeamRepository teamRepository, IMapper mapper, INotifier notifier) : base(notifier)
    {
        _personRepository = personRepository;
        _teamRepository = teamRepository;
        _mapper = mapper;
    }

    public override async Task<Response> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = _mapper.Map<Person>(request);

        var personBefore = await _personRepository.FindAsync(personFilter => personFilter.Id == person.Id);
        if (personBefore == null)
        {
            Notification("Pessoa não encontrada");
            return new Response();
        }
        person.Active = personBefore.Active;

        if (!ExecuteValidation(new PersonValidation(), person)) return new Response();

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
        return new Response { Entity = person };
    }
}