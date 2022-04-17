using MassTransit;
using System;
using System.Threading.Tasks;

namespace UrbanClap.ServiceProvider.Services
{
    public class MessageBus : IMessageBus
    {
        private readonly IBusControl _messageBus;
        private const string _queue = "booking-confirmation";
        private const string _notifyQueue = "notification";

        public MessageBus(IBusControl messgaeBus)
        {
            _messageBus = messgaeBus ?? throw new ArgumentNullException(nameof(messgaeBus));
        }

        public async Task SendNotifications<T>(T message)
        {
            // send notification to notification service.   
            await SendMessage(message, _notifyQueue);

            // send booking-confirmation message onto the message bus.
            await SendMessage(message, _queue);
        }

        private async Task SendMessage<T>(T message, string queue)
        {
            var messageBusEndPoint = await _messageBus.GetSendEndpoint(new Uri($"queue:{queue}"));
            await messageBusEndPoint.Send(message);
        }
    }
}
