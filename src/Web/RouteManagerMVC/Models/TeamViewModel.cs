using RouteManager.Domain.Entities;
using System.Collections.Generic;

namespace RouteManagerMVC.Models
{
    public class TeamViewModel
    {
        public Team Team { get; set; }
        public virtual IEnumerable<string> PeopleIds { get; set; }
        public virtual IEnumerable<City> Cities { get; set; }
        public virtual IEnumerable<Person> People { get; set; }
    }
}
