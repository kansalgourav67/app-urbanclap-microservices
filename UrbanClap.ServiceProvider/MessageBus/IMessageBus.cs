using System.Threading.Tasks;

namespace UrbanClap.ServiceProvider.Services
{
    public interface IMessageBus
    {
        Task SendNotifications<T>(T message);
    }
}
