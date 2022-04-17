using MassTransit;
using System;
using System.Threading.Tasks;
using UrbanClap.AdministrationService.Repositories;
using UrbanClap.Common.EventBus.Messages;

namespace UrbanClap.AdministrationService.Consumers
{
    /// <summary>
    /// Consumer listens to booking confirmation status with service provider assigned.
    /// </summary>
    public class ServiceBookingConfirmationConsumer : IConsumer<BookingConfirmationMessage>
    {
        private readonly IBookingRepository _bookingRepository;

        public ServiceBookingConfirmationConsumer(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        }

        public async Task Consume(ConsumeContext<BookingConfirmationMessage> context)
        {
            var message = context.Message;

            // update the status of the booking with service provider confirmation.
            if (_bookingRepository.IsBookingExists(message.BookingId))
            {
                var booking = _bookingRepository.GetBookingById(message.BookingId);
                booking.BookingStatus = message.BookingStatus;
                booking.ServiceProviderId = message.ServiceProviderId;
                _bookingRepository.UpdateBooking(booking);
            }

            await Task.CompletedTask;
        }
    }
}
