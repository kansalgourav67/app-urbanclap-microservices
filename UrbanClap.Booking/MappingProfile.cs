using AutoMapper;
using UrbanClap.BookingService.Services;
using UrbanClap.Common.EventBus.Messages;

namespace UrbanClap.BookingService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Booking, ServiceBookingMessage>().ReverseMap();
        }
    }
}
