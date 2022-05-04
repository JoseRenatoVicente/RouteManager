using RouteManager.Domain.Core.Entities.Base;
using System.Collections.Generic;

namespace RouteManager.Domain.Core.Entities.Identity
{
    public class Role : EntityBase
    {
        public string Description { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}
