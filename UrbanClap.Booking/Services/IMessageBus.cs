using System.Threading.Tasks;

namespace UrbanClap.BookingService.Services
{
    public interface IMessageBus
    {
        Task SendMessage<T>(T message);
    }
}
