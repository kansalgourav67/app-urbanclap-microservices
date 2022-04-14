using AutoMapper;
using MassTransit;
using System;
using System.Threading.Tasks;
using UrbanClap.BookingService.Models;
using UrbanClap.BookingService.Repository;

namespace UrbanClap.BookingService.Consumers
{
    public class BookingConfirmationConsumer : IConsumer<BookingConfirmationMessage>
    {
        private readonly IMapper _mapper;

        public BookingConfirmationConsumer(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<BookingConfirmationMessage> context)
        {
            var message = context.Message;

            var booking = BookingRepository.GetBookingById(message.BookingId);
            booking.ServiceProviderId = message.ServiceProviderId;
            booking.BookingStatus = (BookingStatus)message.BookingStatus;

            // UPDATE STATUS AND SERVICE PROVIDER ID TO DATABASE.
            BookingRepository.UpdateBooking(booking);
        }
    }
}
