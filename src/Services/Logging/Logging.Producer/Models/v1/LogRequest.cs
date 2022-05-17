namespace Logging.Producer.Models.v1;

public sealed record LogRequest
{
    public string? UserId { get; init; }
    public string? EntityId { get; init; }
    public object? EntityBefore { get; init; }
    public object? EntityAfter { get; init; }
    public Operation Operation { get; init; }
}
public enum Operation
{
    Create = 0,
    Delete = 1,
    Update = 2
}