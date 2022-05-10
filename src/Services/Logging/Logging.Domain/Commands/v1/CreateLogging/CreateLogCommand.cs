using RouteManager.Domain.Core.Contracts;
using RouteManager.Domain.Core.Entities.Enums;

namespace Logging.Domain.Commands.v1.CreateLogging;

public sealed record CreateLogCommand : IBaseCommand
{
    public string? UserId { get; init; }
    public string? EntityId { get; init; }
    public object? EntityBefore { get; init; }
    public object? EntityAfter { get; init; }
    public Operation Operation { get; init; }
}