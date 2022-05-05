using AutoMapper;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.Cities.Update;

public class UpdateCityCommandProfile : Profile
{
    public UpdateCityCommandProfile()
    {
        CreateMap<UpdateCityCommand, City>().ReverseMap();
    }
}