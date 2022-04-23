using RouteManager.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RouteManager.Domain.Entities
{
    public class Team : EntityBase
    {
        public string Name { get; set; }
        public virtual IEnumerable<Person> People { get; set; }
        public virtual City City { get; set; }
    }
}