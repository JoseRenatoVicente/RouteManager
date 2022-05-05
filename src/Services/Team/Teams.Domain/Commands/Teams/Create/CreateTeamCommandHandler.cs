using AutoMapper;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using Teams.Domain.Contracts.v1;
using Teams.Domain.Entities.v1;
using Teams.Domain.Validations.v1;

namespace Teams.Domain.Commands.Teams.Create;

public class CreateTeamCommandHandler : CommandHandler<CreateTeamCommand>
{
    private readonly ITeamRepository _teamRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public CreateTeamCommandHandler(ITeamRepository teamRepository, ICityRepository cityRepository, IPersonRepository personRepository, IMapper mapper, INotifier notifier) : base(notifier)
    {
        _teamRepository = teamRepository;
        _cityRepository = cityRepository;
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public override async Task<Response> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = _mapper.Map<Team>(request);
        string peopleIds = "";

        if (team.People == null)
        {
            Notification("Nenhuma pessoa selecionada, escolha uma");
            return new Response();
        }

        foreach (var item in team.People)
        {
            peopleIds += item.Id + ",";
        }

        var ids = peopleIds.TrimEnd(',').Split(',');

        team.People = await _personRepository.FindAllAsync(p => ids.Contains(p.Id));
        if (team.People == null)
        {
            Notification("Pessoas não encontradas no sistema");
            return new Response();
        }
        team.People = team.People.Select(c =>
        {
            c.Active = false;
            return c;
        });



        team.City = await _cityRepository.FindAsync(c => c.Id == team.City!.Id);
        if (team.City == null)
        {
            Notification("Cidade não encontrada no sistema");
            return new Response();
        }

        if (!ExecuteValidation(new TeamValidation(), team)) return new Response();

        foreach (var person in team.People)
        {
            await _personRepository.UpdateAsync(person);
        }


        await _teamRepository.AddAsync(team);

        return new Response { Entity = team };
    }
}