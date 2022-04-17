using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ServiceProvider.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanClap.Common.EventBus.Messages;
using UrbanClap.ServiceProvider.Repositories;
using UrbanClap.ServiceProvider.Services;

namespace UrbanClap.ServiceProvider.Controllers
{
    [Route("api/serviceprovider")]
    [ApiController]
    public class ServiceProviderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProviderRepository _serviceProviderRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMessageBus _messageBus;

        public ServiceProviderController(IConfiguration configuration,IMessageBus messageBus, IServiceProviderRepository serviceProviderRepository, IBookingRepository bookingRepository)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _serviceProviderRepository = serviceProviderRepository ?? throw new ArgumentNullException(nameof(serviceProviderRepository));
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
        }

        /// <summary>
        /// Returns all service providers.
        /// </summary>
        [HttpGet]
        public IEnumerable<Provider.ServiceProvider> GetServiceProviders()
        {
            return _serviceProviderRepository.GetServiceProviders();
        }

        /// <summary>
        /// Add or registers new service provider.
        /// </summary>
        [HttpPost]
        public IActionResult AddServiceProvider(Provider.ServiceProvider serviceProvider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var serviceProviderId =_serviceProviderRepository.AddServiceProvider(serviceProvider);
            return Ok(serviceProviderId);
        }

        /// <summary>
        /// Returns service providers who are available in specified pincode and provide required service.
        /// </summary>
        [HttpGet("{serviceType}/{pincode}")]
        public IActionResult GetProvidersByAvailability(string pincode, string serviceType)
        {
            var providers = _serviceProviderRepository.GetProvidersByAvailability(pincode, serviceType);
           return Ok(providers);
        }

        /// <summary>
        /// Returns recent booking related notification as per provider id.
        /// </summary>
        [HttpGet("new/bookings/{providerId}")]
        public IActionResult Get(int providerId)
        {
            if (_serviceProviderRepository.IsServiceProviderExists(providerId))
            {
                return Ok(_bookingRepository.GetBookingsByProviderId(providerId));
            }
            else
            {
                return BadRequest($"Service Provider with id {providerId} does not exists");
            }
        }

        /// <summary>
        /// Service Provider accept or deny the service booking.
        /// </summary>
        [HttpPost("action/{providerAction}")]
        public async Task<IActionResult> Post(string providerAction, [FromBody] BookingDetails _bookingDetails)
        {
            if (!_bookingRepository.IsBookingExists(_bookingDetails.BookingId) || !_serviceProviderRepository.IsServiceProviderExists(_bookingDetails.ServiceProviderId))
            {
                return BadRequest();
            }

            if (!providerAction.Equals("accept", StringComparison.OrdinalIgnoreCase) && !providerAction.Equals("deny", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid input. Please accept or deny");
            }

            // respond to booking request by accepting or denying it.
            var bookingDetails = _bookingRepository.GetBookingById(_bookingDetails.BookingId);
            bookingDetails.BookingStatus = providerAction.Equals("accept", StringComparison.OrdinalIgnoreCase) ? BookingStatus.Accepted : BookingStatus.Rejected;
            _bookingRepository.UpdateBookingStatus(bookingDetails);

            var provider = _serviceProviderRepository.GetProviderById(_bookingDetails.ServiceProviderId);

            if (providerAction.Equals("accept", StringComparison.OrdinalIgnoreCase))
            {
                // send confirmation message to notification, admin and booking service informing them about service provider assigned and service provider details.
                var bookingConfirmationMessage = new BookingConfirmationMessage
                {
                    BookingId = bookingDetails.BookingId,
                    CustomerId = bookingDetails.CustomerId,
                    ProviderName = provider.Name,
                    ProviderAddress = provider.Address,
                    ProviderContactNumber = provider.MobileNumber,
                    ServiceProviderId = provider.Id,
                    BookingStatus = BookingStatus.Accepted,
                    Cost = bookingDetails.Cost
                };

                await _messageBus.SendNotifications(bookingConfirmationMessage);

                return Ok(String.Format("Thanks for confirmation. Your job is {0}", bookingDetails.JobDescription));
            }

            return Ok("Cancelled successfully");
        }
    }
}
