using AutoMapper;
using Logging.Domain.Entities.v1;

namespace Logging.Domain.Commands.Create;

public class CreateLogCommandProfile : Profile
{
    public CreateLogCommandProfile()
    {
        CreateMap<CreateLogCommand, Log>().ReverseMap();
    }
}