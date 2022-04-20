using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace RouteManager.WebAPI.Core.Notifications
{
    public class NotificationFilter : IAsyncResultFilter
	{
		protected readonly INotifier _notifier;

        public NotificationFilter(INotifier notifier)
        {
            _notifier = notifier;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			if (_notifier.IsNotified())
			{
				context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				context.HttpContext.Response.ContentType = "application/json";

				var notifications = JsonSerializer.Serialize(_notifier.GetNotifications());
				await context.HttpContext.Response.WriteAsync(notifications);

				return;
			}

			await next();
		}
	}
}
