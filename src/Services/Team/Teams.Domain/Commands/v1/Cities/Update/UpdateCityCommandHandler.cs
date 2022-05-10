using AutoMapper;
using MongoDB.Driver;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using Teams.Domain.Contracts.v1;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.v1.Cities.Update;

public sealed class UpdateCityCommandHandler : ICommandHandler<UpdateCityCommand>
{
    private readonly ITeamRepository _teamRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IMapper _mapper;
    private readonly INotifier _notifier;

    public UpdateCityCommandHandler(ITeamRepository teamRepository, ICityRepository cityRepository, IMapper mapper, INotifier notifier)
    {
        _teamRepository = teamRepository;
        _cityRepository = cityRepository;
        _mapper = mapper;
        _notifier = notifier;
    }
    public async Task<Response> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
    {
        var city = _mapper.Map<City>(request);

        var cityBefore = await _cityRepository.FindAsync(cityFilter => cityFilter.Id == city.Id);

        if (cityBefore == null)
        {
            _notifier.Handle("Cidade não encontrada");
            return new Response();
        }

        var filterDefinition = Builders<Team>.Filter.Eq(p => p.City!.Id, cityBefore.Id);
        var updateDefinition = Builders<Team>.Update.Set(p => p.City, city);

        await _teamRepository.UpdateAllAsync(filterDefinition, updateDefinition);
        await _cityRepository.UpdateAsync(city);
        return new Response { Content = city };
    }
}