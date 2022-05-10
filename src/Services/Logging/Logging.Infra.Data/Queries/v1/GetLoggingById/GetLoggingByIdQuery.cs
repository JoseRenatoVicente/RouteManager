using RouteManager.Domain.Core.Contracts;

namespace Logging.Infra.Data.Queries.v1.GetLoggingById;

public sealed record GetLoggingByIdQuery(string Id) : IBaseQuery;