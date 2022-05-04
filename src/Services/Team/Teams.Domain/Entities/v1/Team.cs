using RouteManager.Domain.Core.Entities.Base;

namespace Teams.Domain.Entities.v1
{
    public class Team : EntityBase
    {
        public string Name { get; set; }
        public virtual IEnumerable<Person> People { get; set; }
        public virtual City City { get; set; }
    }
}