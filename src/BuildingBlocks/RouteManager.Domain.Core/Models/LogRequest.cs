using RouteManager.Domain.Core.Entities;
using RouteManager.Domain.Core.Entities.Enums;
using RouteManager.Domain.Core.Entities.Identity;
using System.Text.Json;

namespace RouteManager.Domain.Core.Models;

public class LogRequest
{
    public LogRequest(Log log)
    {
        User = log.User;
        EntityId = log.EntityId;
        EntityBefore = JsonSerializer.Deserialize<object>(log.EntityBefore);
        EntityAfter = JsonSerializer.Deserialize<object>(log.EntityAfter);
        Operation = log.Operation;
    }
    public LogRequest()
    {

    }

    public string Id { get; set; }
    public User User { get; init; }
    public string EntityId { get; init; }
    public object EntityBefore { get; init; }
    public object EntityAfter { get; init; }
    public Operation Operation { get; init; }

}

