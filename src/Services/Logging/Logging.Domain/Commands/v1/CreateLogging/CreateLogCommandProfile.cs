using AutoMapper;
using Logging.Domain.Entities.v1;

namespace Logging.Domain.Commands.v1.CreateLogging;

public sealed class CreateLogCommandProfile : Profile
{
    public CreateLogCommandProfile()
    {
        CreateMap<CreateLogCommand, Log>().ReverseMap();
    }
}