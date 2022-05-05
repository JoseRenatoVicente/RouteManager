using AutoMapper;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.People.Create;

public class CreatePersonCommandProfile : Profile
{
    public CreatePersonCommandProfile()
    {
        CreateMap<CreatePersonCommand, Person>().ReverseMap();
    }
}