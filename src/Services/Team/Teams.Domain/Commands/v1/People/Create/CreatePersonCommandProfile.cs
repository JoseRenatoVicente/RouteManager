using AutoMapper;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.v1.People.Create;

public sealed class CreatePersonCommandProfile : Profile
{
    public CreatePersonCommandProfile()
    {
        CreateMap<CreatePersonCommand, Person>().ReverseMap();
    }
}