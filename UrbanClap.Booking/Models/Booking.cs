using System;
using UrbanClap.Common.EventBus.Messages;

namespace UrbanClap.BookingService.Models
{
    /// <summary>
    /// Contains all the informations related to service booking.
    /// </summary>
    public class Booking
    {
        public int BookingId { get; set; }

        public int CustomerId { get; set; }

        public int ServiceId { get; set; }

        public string ServiceType { get; set; }

        public string JobDescription { get; set; }

        public DateTime DateTime { get; set; }

        public string MobileNumber { get; set; }

        public string Address { get; set; }

        public string PinCode { get; set; }

        public float Cost { get; set; }

        public int ServiceProviderId { get; set; }

        public BookingStatus BookingStatus { get; set; }
    }
}
