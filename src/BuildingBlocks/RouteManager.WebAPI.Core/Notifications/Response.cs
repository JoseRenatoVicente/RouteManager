using System.Collections.Generic;

namespace RouteManager.WebAPI.Core.Notifications;

public class Response
{
    public bool IsSuccess { get; set; } = true;
    public IEnumerable<string> Errors { get; set; }
    public object Content { get; set; }
}