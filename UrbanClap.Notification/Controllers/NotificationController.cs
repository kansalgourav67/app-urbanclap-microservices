using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanClap.NotificationService.Repositories;

namespace UrbanClap.Notification.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
        }

        /// <summary>
        /// Returns all notification as per customer id.
        /// </summary>
        [HttpGet("{customerid}")]
        public IEnumerable<NotificationService.Models.Notification> Get(int customerid)
        {
            return notificationRepository.GetNotifications(customerid);
        }
    }
}
