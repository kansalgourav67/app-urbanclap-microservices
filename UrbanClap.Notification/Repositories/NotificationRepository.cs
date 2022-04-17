using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrbanClap.NotificationService.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        public static List<Models.Notification> _notifications = new List<Models.Notification>();

        /// </inheritdoc>
        public void AddNotification(Models.Notification notification)
        {
            notification.Id = new Random().Next();
            _notifications.Add(notification);
        }

        /// </inheritdoc>
        public List<Models.Notification> GetNotifications(int customerid)
        {
            return _notifications.FindAll(n => n.UserId == customerid);
        }
    }
}
