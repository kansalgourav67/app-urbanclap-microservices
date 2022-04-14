using MassTransit;
using System;
using System.Threading.Tasks;

namespace UrbanClap.BookingService.Services
{
    public class MessageBus : IMessageBus
    {
        private readonly IBusControl _messageBus;
        private const string _queue = "booking";

        public MessageBus(IBusControl messgaeBus)
        {
            _messageBus = messgaeBus ?? throw new ArgumentNullException(nameof(messgaeBus));
        }

        public async Task SendMessage<T>(T message)
        {
            var messageBusEndPoint = await _messageBus.GetSendEndpoint(new Uri($"queue:{_queue}"));
            await messageBusEndPoint.Send(message);
        }
    }
}
