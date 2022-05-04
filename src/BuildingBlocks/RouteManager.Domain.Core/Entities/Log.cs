﻿using RouteManager.Domain.Core.DTO;
using RouteManager.Domain.Core.Entities.Base;
using RouteManager.Domain.Core.Entities.Enums;
using RouteManager.Domain.Core.Entities.Identity;
using System;
using System.Text.Json;

namespace RouteManager.Domain.Core.Entities
{
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
        public Log()
        {

        }

        public User User { get; set; }
        public string EntityId { get; set; }
        public string EntityBefore { get; set; }
        public string EntityAfter { get; set; }
        public Operation Operation { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
