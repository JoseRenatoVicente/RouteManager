using RouteManager.Domain.Core.Entities.Base;
using RouteManager.Domain.Core.Entities.Enums;
using RouteManager.Domain.Core.Models;
using System.Text.Json;

namespace Logging.Domain.Entities.v1;

public sealed class Log : EntityBase
{
    public Log(LogRequest logRequest)
    {
        UserId = logRequest.UserId;
        EntityId = logRequest.EntityId;
        EntityBefore = JsonSerializer.Serialize(logRequest.EntityBefore);
        EntityAfter = JsonSerializer.Serialize(logRequest.EntityAfter);
        Operation = logRequest.Operation;
    }
    public Log()
    {

    }

    public string? UserId { get; set; }
    public string? EntityId { get; set; }
    public string? EntityBefore { get; set; }
    public string? EntityAfter { get; set; }
    public Operation Operation { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;

}