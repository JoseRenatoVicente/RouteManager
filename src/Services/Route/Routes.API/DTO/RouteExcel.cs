using System.Collections.Generic;
using System.Text;

namespace Routes.API.DTO
{
    public class RouteExcel
    {
        public List<List<string>> Table { get; set; }
        public StringBuilder TableHTML { get; set; } = new StringBuilder();
        public IEnumerable<string> Columns { get; set; }
    }
}
