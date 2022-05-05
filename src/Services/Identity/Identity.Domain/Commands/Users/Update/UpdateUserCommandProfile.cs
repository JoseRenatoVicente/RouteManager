using AutoMapper;
using Identity.Domain.Entities.v1;

namespace Identity.Domain.Commands.Users.Update
{
    public class UpdateUserCommandProfile : Profile
    {
        public UpdateUserCommandProfile()
        {
            CreateMap<UpdateUserCommand, User>().ReverseMap();
        }
    }
}
