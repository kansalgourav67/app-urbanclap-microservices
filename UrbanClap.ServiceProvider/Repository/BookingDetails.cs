using System;
using UrbanClap.ServiceProvider.Repository;

namespace ServiceProvider.Repository
{
    public class BookingDetails
    {
        public int ServiceProviderId { get; set; }

        public int BookingId { get; set; }

        public int CustomerId { get; set; }

        public string ServiceType { get; set; }

        public string JobDescription { get; set; }

        public DateTime DateTime { get; set; }

        public string MobileNumber { get; set; }

        public string Address { get; set; }

        public string PinCode { get; set; }

        public float Cost { get; set; }

        public BookingStatus Status { get; set; }
    }
}
