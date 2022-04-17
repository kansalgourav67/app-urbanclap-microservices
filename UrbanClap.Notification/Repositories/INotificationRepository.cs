using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrbanClap.NotificationService.Repositories
{
    /// <summary>
    /// Persist notification for now as we are not using any 3rd party library.
    /// </summary>
    public interface INotificationRepository
    {
        /// <summary>
        /// Add new notification.
        /// </summary>
        void AddNotification(Models.Notification notification);

        /// <summary>
        /// Returns all notifications by customer id.
        /// </summary>
        List<Models.Notification> GetNotifications(int customerid);
    }
}
