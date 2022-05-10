using AutoMapper;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using Teams.Domain.Contracts.v1;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.v1.Cities.Create;

public sealed class CreateCityCommandHandler : ICommandHandler<CreateCityCommand>
{
    private readonly ICityRepository _cityRepository;
    private readonly IMapper _mapper;

    public CreateCityCommandHandler(IMapper mapper, ICityRepository cityRepository)
    {
        _mapper = mapper;
        _cityRepository = cityRepository;
    }

    public async Task<Response> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var city = _mapper.Map<City>(request);

        await _cityRepository.AddAsync(city);
        return new Response { Entity = city };
    }
}