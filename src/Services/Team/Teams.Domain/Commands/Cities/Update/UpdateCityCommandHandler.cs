using AutoMapper;
using MongoDB.Driver;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using Teams.Domain.Contracts.v1;
using Teams.Domain.Entities.v1;
using Teams.Domain.Validations.v1;

namespace Teams.Domain.Commands.Cities.Update;

public class UpdateCityCommandHandler : CommandHandler<UpdateCityCommand>
{
    private readonly ITeamRepository _teamRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IMapper _mapper;

    public UpdateCityCommandHandler(ITeamRepository teamRepository, ICityRepository cityRepository, IMapper mapper, INotifier notifier) : base(notifier)
    {
        this._teamRepository = teamRepository;
        this._cityRepository = cityRepository;
        this._mapper = mapper;
    }

    public override async Task<Response> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
    {
        var city = _mapper.Map<City>(request);

        var cityBefore = await _cityRepository.FindAsync(cityFilter => cityFilter.Id == city.Id);

        if (cityBefore == null)
        {
            Notification("Cidade não encontrada");
            return new Response();
        }

        if (!ExecuteValidation(new CityValidation(), city)) return new Response();

        var filterDefinition = Builders<Team>.Filter.Eq(p => p.City!.Id, cityBefore.Id);
        var updateDefinition = Builders<Team>.Update.Set(p => p.City, city);

        await _teamRepository.UpdateAllAsync(filterDefinition, updateDefinition);
        await _cityRepository.UpdateAsync(city);
        return new Response { Entity = city };
    }
}