using RouteManager.Domain.Core.Entities.Base;

namespace Routes.Domain.Entities.v1
{
    public class Route : EntityBase
    {
        public string OS { get; set; }
        public string Base { get; set; }
        public string Service { get; set; }
        public Address Address { get; set; }
    }
}
