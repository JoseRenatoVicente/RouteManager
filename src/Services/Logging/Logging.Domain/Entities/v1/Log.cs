using Logging.Domain.Entities.v1.Enums;
using RouteManager.Domain.Core.Entities.Base;

namespace Logging.Domain.Entities.v1;

public sealed class Log : EntityBase
{
    public string? UserId { get; set; }
    public string? EntityId { get; set; }
    public string? EntityBefore { get; set; }
    public string? EntityAfter { get; set; }
    public Operation Operation { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;

}