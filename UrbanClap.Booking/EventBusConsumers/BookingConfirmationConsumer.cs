using AutoMapper;
using MassTransit;
using System;
using System.Threading.Tasks;
using UrbanClap.BookingService.Models;
using UrbanClap.BookingService.Repository;
using UrbanClap.Common.EventBus.Messages;

namespace UrbanClap.BookingService.Consumers
{
    /// <summary>
    /// Consumer listens to booking confirmation event and update booking status, service provider details.
    /// </summary>
    public class BookingConfirmationConsumer : IConsumer<BookingConfirmationMessage>
    {
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;

        public BookingConfirmationConsumer(IMapper mapper, IBookingRepository bookingRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        }

        public async Task Consume(ConsumeContext<BookingConfirmationMessage> context)
        {
            var message = context.Message;

            var booking = _bookingRepository.GetBookingById(message.BookingId);
            booking.ServiceProviderId = message.ServiceProviderId;
            booking.BookingStatus = message.BookingStatus;

            // update status and service provider id to database.
            _bookingRepository.UpdateBooking(booking);

            await Task.CompletedTask;
        }
    }
}
