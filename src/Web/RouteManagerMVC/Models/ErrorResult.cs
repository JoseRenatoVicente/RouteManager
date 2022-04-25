using System.Collections.Generic;

namespace RouteManagerMVC.Models
{
    public class ErrorResult
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
