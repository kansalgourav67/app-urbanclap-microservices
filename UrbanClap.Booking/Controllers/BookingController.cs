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

namespace UrbanClap.BookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IMessageBus _messageBus;

        private const string BookingOrderAcceptedMessage = "Booking Id is {0} ,Service provider will be assigned shortly";

        public BookingController(HttpClient httpClient, IConfiguration configuration, IMapper mapper, IMessageBus messageBus)
        {
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Handles and process new service booking order.
        /// </summary>
        /// <param name="bookingDetails">
        /// Model contains booking related info.
        /// </param>
        [HttpPost]
        public async Task<IActionResult> Post(Models.Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                // Gets and set the latest price/cost of service from ServiceCatalog service.
                var response = await _client.GetAsync($"{_configuration["ServiceCatalog:HostAddress"]}/api/services/price/{booking.ServiceType}");
                var serviceCost = await HandleHttpResponse<float>(response);
                booking.Cost = serviceCost;

                // set status of booking to received, and after providing service to customer will set it to completed.
                booking.BookingStatus = BookingStatus.Received;

                // Create new booking in the database.
                var bookingId = BookingRepository.AddBooking(booking);

                // send the booking notification to admin, to assign the service provider.
                // send the BookingDetails message onto the message bus.
                BookingDetailsMessage bookingDetailsMessage = _mapper.Map<BookingDetailsMessage>(booking);
                bookingDetailsMessage.BookingId = bookingId;
                await _messageBus.SendMessage(bookingDetailsMessage);

                return Accepted(string.Format(BookingOrderAcceptedMessage, bookingId));
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
            if (BookingRepository.IsBookingExists(bookingId))
            {
                return Ok(BookingRepository.GetBookingById(bookingId));
            }

            return BadRequest("Invalid Booking Id");
        }

        /// <summary>
        /// Returns all the bookings done by the customers till date.
        /// </summary>
        [HttpGet("customerId/{customerId}")]
        public List<Models.Booking> GetBookingsByCustomer(int customerId)
        {
            return BookingRepository.GetAllBookingsDoneByCustomers(customerId);
        }

        /// <summary>
        /// Deserialized http response and returns generic type object.
        /// </summary>
        private static async Task<T> HandleHttpResponse<T>(HttpResponseMessage httpResponse)
        {
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                var content = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            else
            {
                throw new Exception("Someting wrng happens. Please try again");
            }
        }
    }
}