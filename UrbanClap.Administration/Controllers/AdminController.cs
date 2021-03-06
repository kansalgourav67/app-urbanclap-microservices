using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using UrbanClap.AdministrationService.Repositories;
using AutoMapper;
using UrbanClap.AdministrationService.Services;
using UrbanClap.Common.EventBus.Messages;
using IServiceProvider = UrbanClap.AdministrationService.Services.IServiceProvider;

namespace UrbanClap.AdministrationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IBookingRepository _bookingRepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageBus _messageBus;
        private readonly IMapper _mapper;

        public AdminController(IMessageBus messageBus, IMapper mapper, IConfiguration config, IBookingRepository bookingRepository, IServiceProvider serviceProvider)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _configuration = config ?? throw new ArgumentNullException(nameof(config));
            _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Returns all recent bookings that have status code received.
        /// </summary>
        [HttpGet("new/bookings")]
        public IActionResult Get()
        {
            return Ok(_bookingRepository.GetAllRecentBookings());
        }

        /// <summary>
        /// Assign the service provider to new booking request.
        /// </summary>
        [HttpPost("assign/serviceprovider/{bookingId}")]
        public async Task<IActionResult> Post(int bookingId)
        {
            try
            {
                if (!_bookingRepository.IsBookingExists(bookingId))
                {
                    return BadRequest($"Booking with id {bookingId} doesn't exists");
                }
                else
                {
                    var booking = _bookingRepository.GetBookingById(bookingId);

                    // Get all service providers that are available in specified pincode and provide requested service eg. Electrician.
                    var serviceProviders = await _serviceProvider.GetAvailableProviders(booking.ServiceType, booking.PinCode);
                    if (serviceProviders.Count >= 1)
                    {
                        // assign service to available service provider.
                        // assigning booking to random service provider.
                        booking.ServiceProviderId = GetRandomServiceProvider(serviceProviders);
                        var message = _mapper.Map<ServiceBookingMessage>(booking);

                        // send the booking with all details to availble service provider.
                        await _messageBus.SendMessage(message);

                        return Ok("Nofitication sent to Available service provider");
                    }
                    else
                    {
                        return Ok("Service providers are busy in your area. Please book another slot");
                    }
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Return random service provider from aviallble providers.
        /// </summary>
        private static int GetRandomServiceProvider(List<int> providers)
        {
            Random random = new Random();
            var randomValue = random.Next(providers.Count);
            return providers[randomValue];
        }
    }
}
