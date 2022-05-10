using AutoMapper;
using Identity.Domain.Entities.v1;

namespace Identity.Domain.Commands.v1.Users.Update;

public sealed class UpdateUserCommandProfile : Profile
{
    public UpdateUserCommandProfile()
    {
        CreateMap<UpdateUserCommand, User>().ReverseMap();
    }
}