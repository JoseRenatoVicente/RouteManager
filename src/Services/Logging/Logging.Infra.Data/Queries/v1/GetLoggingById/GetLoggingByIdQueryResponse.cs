using Logging.Domain.Entities.v1.Enums;

namespace Logging.Infra.Data.Queries.v1.GetLoggingById;

public sealed record GetLoggingByIdQueryResponse
{
    public string? Id { get; init; }
    public string? UserId { get; init; }
    public string? EntityId { get; init; }
    public object? EntityBefore { get; init; }
    public object? EntityAfter { get; init; }
    public Operation Operation { get; init; }
    public DateTime CreationDate { get; init; } = DateTime.Now;
}