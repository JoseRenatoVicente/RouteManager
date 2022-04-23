using RouteManager.Domain.Entities;
using RouteManager.Domain.Entities.Enums;
using RouteManager.Domain.Entities.Identity;
using System.Text.Json;

namespace RouteManager.Domain.DTO
{
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
        public User User { get; set; }
        public string EntityId { get; set; }
        public object EntityBefore { get; set; }
        public object EntityAfter { get; set; }
        public Operation Operation { get; set; }

    }
}
