using RouteManager.Domain.Core.Entities.Base;

namespace Teams.Domain.Entities.v1
{
    public class Person : EntityBase
    {
        public string Name { get; set; }
        public bool Active { get; set; } = true;
    }
}
