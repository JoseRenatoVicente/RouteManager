using System.Collections.Generic;
using System.Linq;

namespace RouteManager.WebAPI.Core.Notifications
{
    public class Notifier : INotifier
    {
        private ICollection<string> _notifications;

        public Notifier()
        {
            _notifications = new List<string>();
        }

        public void Handle(string notification)
        {
            _notifications.Add(notification);
        }

        public IEnumerable<string> GetNotifications()
        {
            return _notifications;
        }

        public bool IsNotified()
        {
            return _notifications.Any();
        }

        public void Clear()
        {
            _notifications.Clear();
        }
    }
}
