using AutoMapper;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.v1.Cities.Create;

public sealed class CreateCityCommandProfile : Profile
{
    public CreateCityCommandProfile()
    {
        CreateMap<CreateCityCommand, City>().ReverseMap();
    }
}