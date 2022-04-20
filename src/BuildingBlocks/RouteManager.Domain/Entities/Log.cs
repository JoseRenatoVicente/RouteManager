using RouteManager.Domain.DTO;
using RouteManager.Domain.Entities.Base;
using RouteManager.Domain.Entities.Enums;
using RouteManager.Domain.Entities.Identity;
using System;
using System.Text.Json;

namespace RouteManager.Domain.Entities
{
    public class Log : EntityBase
    {
        public Log(LogRequest logRequest)
        {
            User = logRequest.User;
            EntityBefore = JsonSerializer.Serialize(logRequest.EntityBefore);
            EntityAfter = JsonSerializer.Serialize(logRequest.EntityAfter);
            Operation = logRequest.Operation;
        }
        public Log()
        {

        }

        public User User { get; set; }
        public string EntityBefore { get; set; }
        public string EntityAfter { get; set; }
        public Operation Operation { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
