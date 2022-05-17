using AutoMapper;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using System.Text;
using Teams.Domain.Contracts.v1;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.v1.Teams.Create;

public sealed class CreateTeamCommandHandler : ICommandHandler<CreateTeamCommand>
{
    private readonly ITeamRepository _teamRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly INotifier _notifier;

    public CreateTeamCommandHandler(ITeamRepository teamRepository, ICityRepository cityRepository, IPersonRepository personRepository, IMapper mapper, INotifier notifier)
    {
        _teamRepository = teamRepository;
        _cityRepository = cityRepository;
        _personRepository = personRepository;
        _mapper = mapper;
        _notifier = notifier;
    }

    public async Task<Response> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = _mapper.Map<Team>(request);
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
        team.People = team.People.Select(c =>
        {
            c.Active = false;
            return c;
        });



        team.City = await _cityRepository.FindAsync(c => c.Id == team.City!.Id);
        if (team.City == null)
        {
            _notifier.Handle("Cidade não encontrada no sistema");
            return new Response();
        }

        foreach (var person in team.People)
        {
            await _personRepository.UpdateAsync(person);
        }


        await _teamRepository.AddAsync(team);

        return new Response { Content = team };
    }
}