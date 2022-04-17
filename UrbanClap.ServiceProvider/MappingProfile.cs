using AutoMapper;
using ServiceProvider.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanClap.Common.EventBus.Messages;

namespace UrbanClap.ServiceProvider
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ServiceBookingMessage, BookingDetails>().ReverseMap();
        }
    }
}
