using AutoMapper;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.Teams.Create;

public class CreateTeamCommandProfile : Profile
{
    public CreateTeamCommandProfile()
    {
        CreateMap<CreateTeamCommand, Team>().ReverseMap();
    }
}