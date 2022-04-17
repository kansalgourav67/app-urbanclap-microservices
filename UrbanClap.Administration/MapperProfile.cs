using AutoMapper;
using UrbanClap.AdministrationService.Repositories;
using UrbanClap.Common.EventBus.Messages;

namespace UrbanClap.AdministrationService
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ServiceBookingMessage, Booking>().ReverseMap();
        }
    }
}
