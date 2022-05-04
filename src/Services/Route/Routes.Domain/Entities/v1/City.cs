using RouteManager.Domain.Core.Entities.Base;

namespace Routes.Domain.Entities.v1
{
    public class City : EntityBase
    {
        public string Name { get; set; }
        public string State { get; set; }

    }
}
