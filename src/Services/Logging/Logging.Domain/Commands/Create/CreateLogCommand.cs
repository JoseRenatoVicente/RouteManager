using RouteManager.Domain.Core.Contracts;
using RouteManager.Domain.Core.Entities.Enums;
using RouteManager.Domain.Core.Entities.Identity;

namespace Logging.Domain.Commands.Create;

public sealed class CreateLogCommand : IBaseCommand
{
    public User? User { get; }
    public string? EntityId { get; }
    public object? EntityBefore { get; }
    public object? EntityAfter { get; }
    public Operation Operation { get; }
}