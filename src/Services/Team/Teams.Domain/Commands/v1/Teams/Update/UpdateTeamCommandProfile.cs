using AutoMapper;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.v1.Teams.Update;

public class UpdateTeamCommandProfile : Profile
{
    public UpdateTeamCommandProfile()
    {
        CreateMap<UpdateTeamCommand, Team>().ReverseMap();
    }
}