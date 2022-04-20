using RouteManager.Domain.Entities;
using System.Collections.Generic;

namespace GeradorRotasMVC.Models
{
    public class EquipeViewModel 
    {
        public Team Team { get; set; }
        public virtual IEnumerable<City> Cities { get; set; }
        public virtual IEnumerable<Person> People { get; set; }
    }
}
