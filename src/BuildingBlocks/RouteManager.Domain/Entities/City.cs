using RouteManager.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RouteManager.Domain.Entities
{
    public class City : EntityBase
    {
        public string Name { get; set; }
        public string State { get; set; }
        
    }
}
