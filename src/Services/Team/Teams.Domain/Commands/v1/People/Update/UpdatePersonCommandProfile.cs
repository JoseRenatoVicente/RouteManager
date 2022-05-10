using AutoMapper;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.v1.People.Update;

public sealed class UpdatePersonCommandProfile : Profile
{
    public UpdatePersonCommandProfile()
    {
        CreateMap<UpdatePersonCommand, Person>().ReverseMap();
    }
}