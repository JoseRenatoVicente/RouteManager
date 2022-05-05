using RouteManager.Domain.Core.Entities.Base;
using RouteManager.Domain.Core.Entities.Enums;
using RouteManager.Domain.Core.Entities.Identity;
using RouteManager.Domain.Core.Models;
using System;
using System.Text.Json;

namespace RouteManager.Domain.Core.Entities;

public class Log : EntityBase
{
    public Log(LogRequest logRequest)
    {
        User = logRequest.User;
        EntityId = logRequest.EntityId;
        EntityBefore = JsonSerializer.Serialize(logRequest.EntityBefore);
        EntityAfter = JsonSerializer.Serialize(logRequest.EntityAfter);
        Operation = logRequest.Operation;
    }

    public User User { get; }
    public string EntityId { get; }
    public string EntityBefore { get; }
    public string EntityAfter { get; }
    public Operation Operation { get; }
    public DateTime CreationDate { get; set; } = DateTime.Now;

}
