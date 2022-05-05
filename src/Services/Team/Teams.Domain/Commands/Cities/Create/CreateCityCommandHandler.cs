using AutoMapper;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using Teams.Domain.Contracts.v1;
using Teams.Domain.Entities.v1;
using Teams.Domain.Validations.v1;

namespace Teams.Domain.Commands.Cities.Create;

public class CreateCityCommandHandler : CommandHandler<CreateCityCommand>
{

    private readonly ICityRepository _cityRepository;
    private readonly IMapper _mapper;

    public CreateCityCommandHandler(IMapper mapper, ICityRepository cityRepository, INotifier notifier) : base(notifier)
    {
        _mapper = mapper;
        _cityRepository = cityRepository;
    }

    public override async Task<Response> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var city = _mapper.Map<City>(request);

        if (!ExecuteValidation(new CityValidation(), city)) return new Response();

        await _cityRepository.AddAsync(city);
        return new Response { Entity = city };
    }
}