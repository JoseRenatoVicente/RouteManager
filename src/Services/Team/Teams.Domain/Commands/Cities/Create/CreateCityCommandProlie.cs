using AutoMapper;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.Cities.Create;

public class CreateCityCommandProlie : Profile
{
    public CreateCityCommandProlie()
    {
        CreateMap<CreateCityCommand, City>().ReverseMap();
    }
}