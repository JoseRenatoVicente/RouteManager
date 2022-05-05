using AutoMapper;
using Teams.Domain.Entities.v1;

namespace Teams.Domain.Commands.People.Update;

public class UpdatePersonCommandProfile : Profile
{
    public UpdatePersonCommandProfile()
    {
        CreateMap<UpdatePersonCommand, Person>().ReverseMap();
    }
}