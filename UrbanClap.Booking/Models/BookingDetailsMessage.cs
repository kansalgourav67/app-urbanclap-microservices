using System;
using UrbanClap.BookingService.Models;

namespace UrbanClap.BookingService.Services
{
    /// <summary>
    /// Message defines booking details that is to be send on Message bus.
    /// </summary>
    public class BookingDetailsMessage
    {
        public int BookingId { get; set; }

        public int CustomerId { get; set; }

        public string ServiceType { get; set; }

        public int ServiceProviderId { get; set; }

        public string JobDescription { get; set; }

        public DateTime DateTime { get; set; }

        public string MobileNumber { get; set; }

        public string Address { get; set; }

        public string PinCode { get; set; }

        public float Cost { get; set; }

        public BookingStatus BookingStatus { get; set; }
    }
}
