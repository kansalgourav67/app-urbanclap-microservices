using AutoMapper;
using MassTransit;
using ServiceProvider.Repository;
using System;
using System.Threading.Tasks;
using UrbanClap.Common.EventBus.Messages;
using UrbanClap.ServiceProvider.Repositories;

namespace UrbanClap.ServiceProvider.Consumers
{
    /// <summary>
    /// Consumer class listen to new booking request.
    /// </summary>
    public class AssignServiceBookingConsumer : IConsumer<ServiceBookingMessage>
    {
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;

        public AssignServiceBookingConsumer(IMapper mapper, IBookingRepository bookingRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        }

        public async Task Consume(ConsumeContext<ServiceBookingMessage> context)
        {
            var bookingMesage = context.Message;

            // persist this booking in database so that service provider can access and respond to it.
            var bookingDetails = _mapper.Map<BookingDetails>(bookingMesage);
            _bookingRepository.AddBooking(bookingDetails);

            await Task.CompletedTask;
        }
    }
}
