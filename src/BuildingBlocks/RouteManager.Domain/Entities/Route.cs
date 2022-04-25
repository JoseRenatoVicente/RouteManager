using RouteManager.Domain.Entities.Base;

namespace RouteManager.Domain.Entities
{
    public class Route : EntityBase
    {
        public string OS { get; set; }
        public string Base { get; set; }
        public string Service { get; set; }
        public Address Address { get; set; }
    }
}
