using RouteManager.Domain.Entities.Base;

namespace RouteManager.Domain.Entities
{
    public class Person : EntityBase
    {
        public string Name { get; set; }
        public bool Active { get; set; } = true;
    }
}
