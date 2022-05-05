using AutoMapper;
using Identity.Domain.Entities.v1;

namespace Identity.Domain.Commands.Users.Create
{
    public class CreateUserCommandProfile : Profile
    {
        public CreateUserCommandProfile()
        {
            CreateMap<CreateUserCommand, User>().ReverseMap();
        }
    }
}
