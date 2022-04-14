using AutoMapper;
using UrbanClap.BookingService.Services;

namespace UrbanClap.BookingService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Booking, BookingDetailsMessage>().ReverseMap();
        }
    }
}
