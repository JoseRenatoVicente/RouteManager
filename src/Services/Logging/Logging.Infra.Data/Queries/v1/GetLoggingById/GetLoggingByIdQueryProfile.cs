using AutoMapper;
using Logging.Domain.Entities.v1;

namespace Logging.Infra.Data.Queries.v1.GetLoggingById;

public sealed class GetLoggingByIdQueryProfile : Profile
{
    public GetLoggingByIdQueryProfile()
    {
        CreateMap<GetLoggingByIdQueryResponse, Log>().ReverseMap();
    }
}