namespace UrbanClap.BookingService.Models
{
    public class BookingConfirmationMessage
    {
        public int CustomerId { get; set; }

        public int BookingId { get; set; }

        public int ServiceProviderId { get; set; }

        public string ServiceProviderName { get; set; }

        public string ServiceProviderContactNumber { get; set; }

        public float Cost { get; set; }

        public BookingStatus BookingStatus { get; set; }

    }
}
