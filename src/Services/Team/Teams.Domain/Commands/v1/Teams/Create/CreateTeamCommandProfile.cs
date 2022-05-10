using AutoMapper;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.v1.Teams.Create;

public sealed class CreateTeamCommandProfile : Profile
{
    public CreateTeamCommandProfile()
    {
        CreateMap<CreateTeamCommand, Team>().ReverseMap();
    }
}