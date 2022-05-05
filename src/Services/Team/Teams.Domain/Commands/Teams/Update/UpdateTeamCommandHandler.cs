using AutoMapper;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using Teams.Domain.Contracts.v1;
using Teams.Domain.Entities.v1;
using Teams.Domain.Validations.v1;

namespace Teams.Domain.Commands.Teams.Update;

public class UpdateTeamCommandHandler : CommandHandler<UpdateTeamCommand>
{

    private readonly ICityRepository _cityRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public UpdateTeamCommandHandler(ICityRepository cityRepository, IPersonRepository personRepository, ITeamRepository teamRepository, IMapper mapper, INotifier notifier) : base(notifier)
    {
        _cityRepository = cityRepository;
        _personRepository = personRepository;
        _teamRepository = teamRepository;
        _mapper = mapper;
    }

    public override async Task<Response> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = _mapper.Map<Team>(request);

        var teamBefore = await _teamRepository.FindAsync(c => c.Id == team.Id);

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

        team.City = await _cityRepository.FindAsync(c => c.Id == team.City!.Id);
        if (team.City == null)
        {
            Notification("Cidade não encontrada no sistema");
            return new Response();
        }

        if (!ExecuteValidation(new TeamValidation(), team)) return new Response();

        foreach (var person in teamBefore.People!.Where(c => !ids.Contains(c.Id)))
        {
            person.Active = false;
            await _personRepository.UpdateAsync(person);
        }


        await _teamRepository.UpdateAsync(team);
        return new Response { Entity = team };
    }
}