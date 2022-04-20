using RouteManager.Domain.Entities.Base;
using System.Collections.Generic;

namespace RouteManager.Domain.Entities.Identity
{
    public class Role : EntityBase
    {
        public string Description { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}
