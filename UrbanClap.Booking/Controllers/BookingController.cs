using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UrbanClap.BookingService.Models;
using UrbanClap.BookingService.Repository;
using UrbanClap.BookingService.Services;
using UrbanClap.Common.EventBus.Messages;

namespace UrbanClap.BookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IMessageBus _messageBus;
        private readonly IBookingRepository _bookingRepository;

        private const string BookingOrderAcceptedMessage = "Thanks!. Your Booking Id is {0} ,Service provider will be assigned shortly";

        public BookingController(ICatalogService catalogService, IConfiguration configuration, IMapper mapper, IMessageBus messageBus, IBookingRepository bookingRepository)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        }

        /// <summary>
        /// Handles and process new service booking order.
        /// </summary>
        /// <param name="bookingDetails">
        /// Model contains booking related info.
        /// </param>
        [HttpPost]
        public async Task<IActionResult> Post(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                // Gets and set the latest price/cost of service like hair cut from ServiceCatalog api.
                booking.Cost = await _catalogService.GetServiceCost(booking.ServiceId);

                // set status of booking to received, and after providing service to customer will set it to completed.
                booking.BookingStatus = BookingStatus.Received;

                // Create new booking in the database.
                var bookingId = _bookingRepository.AddBooking(booking);

                // send the booking notification to admin, to assign the service provider.
                // send the BookingDetails message onto the message bus.
                var bookingDetailsMessage = _mapper.Map<ServiceBookingMessage>(booking);
                bookingDetailsMessage.BookingId = bookingId;
                await _messageBus.SendMessage(bookingDetailsMessage);

                return Ok(string.Format(BookingOrderAcceptedMessage, bookingId));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Returns the booking information as per booking id.
        /// </summary>
        [HttpGet("bookingId/{bookingId}")]
        public IActionResult GetBookingById(int bookingId)
        {
            if (_bookingRepository.IsBookingExists(bookingId))
            {
                return Ok(_bookingRepository.GetBookingById(bookingId));
            }

            return BadRequest("Invalid Booking Id");
        }

        /// <summary>
        /// Returns all the bookings done by the customers till date.
        /// </summary>
        [HttpGet("customerId/{customerId}")]
        public List<Models.Booking> GetBookingsByCustomer(int customerId)
        {
            return _bookingRepository.GetAllBookingsDoneByCustomers(customerId);
        }
    }
}