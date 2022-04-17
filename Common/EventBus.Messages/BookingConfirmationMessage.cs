namespace UrbanClap.Common.EventBus.Messages
{
    public class BookingConfirmationMessage
    {
        public int CustomerId { get; set; }

        public int BookingId { get; set; }

        public int ServiceProviderId { get; set; }

        public string ProviderName { get; set; }

        public string ProviderContactNumber { get; set; }

        public string ProviderAddress { get; set; }

        public float Cost { get; set; }

        public BookingStatus BookingStatus { get; set; }

    }
}
