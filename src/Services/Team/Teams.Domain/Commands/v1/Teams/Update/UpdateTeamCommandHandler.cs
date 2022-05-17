using System.Text;
using AutoMapper;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using Teams.Domain.Contracts.v1;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.v1.Teams.Update;

public class UpdateTeamCommandHandler : ICommandHandler<UpdateTeamCommand>
{

    private readonly ICityRepository _cityRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly INotifier _notifier;

    public UpdateTeamCommandHandler(ICityRepository cityRepository, ITeamRepository teamRepository, IPersonRepository personRepository, IMapper mapper, INotifier notifier)
    {
        _cityRepository = cityRepository;
        _teamRepository = teamRepository;
        _personRepository = personRepository;
        _mapper = mapper;
        _notifier = notifier;
    }

    public async Task<Response> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = _mapper.Map<Team>(request);

        var teamBefore = await _teamRepository.FindAsync(c => c.Id == team.Id);

        StringBuilder peopleIds = new();

        if (team.People == null)
        {
            _notifier.Handle("Nenhuma pessoa selecionada, escolha uma");
            return new Response();
        }

        foreach (var item in team.People)
        {
            peopleIds.Append(item.Id + ",");
        }

        var ids = peopleIds.ToString().TrimEnd(',').Split(',');
        team.People = await _personRepository.FindAllAsync(p => ids.Contains(p.Id));

        if (team.People == null)
        {
            _notifier.Handle("Pessoas não encontradas no sistema");
            return new Response();
        }

        team.City = await _cityRepository.FindAsync(c => c.Id == team.City!.Id);
        if (team.City == null)
        {
            _notifier.Handle("Cidade não encontrada no sistema");
            return new Response();
        }

        foreach (var person in teamBefore.People!.Where(c => !ids.Contains(c.Id)))
        {
            person.Active = false;
            await _personRepository.UpdateAsync(person);
        }

        await _teamRepository.UpdateAsync(team);
        return new Response { Content = team };
    }
}