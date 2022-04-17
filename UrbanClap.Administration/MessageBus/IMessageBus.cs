using System.Threading.Tasks;

namespace UrbanClap.AdministrationService.Services
{
    public interface IMessageBus
    {
        Task SendMessage<T>(T message);
    }
}
