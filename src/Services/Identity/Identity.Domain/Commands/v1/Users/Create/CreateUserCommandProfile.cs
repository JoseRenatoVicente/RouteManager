using AutoMapper;
using Identity.Domain.Entities.v1;

namespace Identity.Domain.Commands.v1.Users.Create;

public sealed class CreateUserCommandProfile : Profile
{
    public CreateUserCommandProfile()
    {
        CreateMap<CreateUserCommand, User>().ReverseMap();
    }
}