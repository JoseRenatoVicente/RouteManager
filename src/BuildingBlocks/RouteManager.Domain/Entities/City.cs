using RouteManager.Domain.Entities.Base;

namespace RouteManager.Domain.Entities
{
    public class City : EntityBase
    {
        public string Name { get; set; }
        public string State { get; set; }

    }
}
