using AutoMapper;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.v1.Cities.Update;

public sealed class UpdateCityCommandProfile : Profile
{
    public UpdateCityCommandProfile()
    {
        CreateMap<UpdateCityCommand, City>().ReverseMap();
    }
}