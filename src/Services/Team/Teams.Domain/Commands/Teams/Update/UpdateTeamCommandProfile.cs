using AutoMapper;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.Teams.Update;

public class UpdateTeamCommandProfile : Profile
{
    public UpdateTeamCommandProfile()
    {
        CreateMap<UpdateTeamCommand, Team>().ReverseMap();
    }
}