using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using UrbanClap.Common.EventBus.Messages;
using UrbanClap.NotificationService.Repositories;

namespace UrbanClap.NotificationService.EventBusConsumers
{
    /// <summary>
    /// Notifcation consumer listen to and log the booking confirmation event.
    /// </summary>
    public class BookingConfirmationNotificationConsumer : IConsumer<BookingConfirmationMessage>
    {
        private readonly ILogger<BookingConfirmationNotificationConsumer> logger;
        private readonly INotificationRepository notificationRepository;

        public BookingConfirmationNotificationConsumer(ILogger<BookingConfirmationNotificationConsumer> logger, INotificationRepository notificationRepository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.notificationRepository = notificationRepository ?? throw new ArgumentException(nameof(notificationRepository));
        }

        public async Task Consume(ConsumeContext<BookingConfirmationMessage> context)
        {
            var message = context.Message;
            logger.LogInformation(message.ToString());

            // persist notification into db.
            var notificationMessage = JsonSerializer.Serialize(message);
            var notification = new Models.Notification
            {
                UserId = message.CustomerId,
                NotificationMessage = notificationMessage
            };

            notificationRepository.AddNotification(notification);

            await Task.CompletedTask;
        }
    }
}
