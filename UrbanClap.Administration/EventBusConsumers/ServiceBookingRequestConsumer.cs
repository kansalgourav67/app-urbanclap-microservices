using AutoMapper;
using MassTransit;
using System;
using System.Threading.Tasks;
using UrbanClap.AdministrationService.Repositories;
using UrbanClap.Common.EventBus.Messages;

namespace UrbanClap.AdministrationService.Consumers
{
    /// <summary>
    /// Consumer listens to new booking request.
    /// </summary>
    public class ServiceBookingRequestConsumer : IConsumer<ServiceBookingMessage>
    {
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;

        public ServiceBookingRequestConsumer(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<ServiceBookingMessage> context)
        {
            var newBooking = context.Message;

            // forward this new booking to repository so that admin can assign service provider to this booking.
           var booking =  _mapper.Map<Booking>(newBooking);
            _bookingRepository.AddBooking(booking);

            await Task.CompletedTask;
        }
    }
}
