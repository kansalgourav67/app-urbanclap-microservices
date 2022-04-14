using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanClap.ServiceProvider.Repository;

namespace ServiceProvider.Repository
{
    public static class ServiceProviderRepository
    {
        public static List<ServiceProvider> _providers = new List<ServiceProvider>
        {
            new ServiceProvider{ Id = 1, Name="Provider 1", Role="Hair Cut", MobileNumber="8180889981", Address="East Delhi", PinCode="101010"},
            new ServiceProvider{ Id = 2, Name="Provider 2", Role="Electrician", MobileNumber="8280889981", Address="West Delhi", PinCode="202020"},
            new ServiceProvider{ Id = 3, Name="Provider 3", Role="Interior Designer", MobileNumber="8380889981", Address="South Delhi", PinCode="303030"},
            new ServiceProvider{ Id = 4, Name="Provider 4", Role="Hair Cut", MobileNumber="9840889981", Address="North Delhi", PinCode="4040404"},
            new ServiceProvider{ Id = 5, Name="Provider 5", Role="Electrician", MobileNumber="9850889981", Address="East Delhi", PinCode="505050"},
            new ServiceProvider{ Id = 6, Name="Provider 6", Role="Electrician", MobileNumber="860889981", Address=" West Delhi", PinCode="606060"},
        };

        public static List<BookingDetails> _bookings = new List<BookingDetails>();

        /// <summary>
        /// Add new service provider.
        /// </summary>
        public static int AddServiceProvider(ServiceProvider serviceProvider)
        {
            serviceProvider.Id = GenerateUniqueId();
            _providers.Add(serviceProvider);
            return serviceProvider.Id;
        }

        /// <summary>
        /// Returns all service providers.
        /// </summary>
        /// <returns></returns>
        public static List<ServiceProvider> GetServiceProviders()
        {
            return _providers;
        }

        /// <summary>
        /// Return service provider by unique id.
        /// </summary>
        public static ServiceProvider GetProviderById(int providerId)
        {
            return _providers.Find(p => p.Id == providerId);
        }

        /// <summary>
        /// Returns id of all service providers by pincode and role.
        /// </summary>
        public static List<int> GetProvidersByAvailability(string pincode, string role)
        {
           return _providers.FindAll(p => p.PinCode == pincode && p.Role.Equals(role, StringComparison.OrdinalIgnoreCase))
                .Select(sp => sp.Id)
                .ToList();
        }

        /// <summary>
        /// Add new booking.
        /// </summary>
        public static int AddBooking(BookingDetails bookingDetails)
        {
            _bookings.Add(bookingDetails);
            return bookingDetails.BookingId;
        }

        /// <summary>
        /// Returns all bookings that are allocated to service provider.
        /// </summary>
        public static List<BookingDetails> GetBookingsByProviderId(int providerId)
        {
            return _bookings.FindAll(b => b.ServiceProviderId == providerId);
        }

        /// <summary>
        /// Update the booking status.
        /// </summary>
        public static int UpdateBookingStatus(BookingDetails bookingDetails)
        {
            var booking = _bookings.Find(b => b.BookingId == bookingDetails.BookingId);
            booking.Status = bookingDetails.Status;
            return bookingDetails.BookingId;
        }

        /// <summary>
        /// Gets the booking status by boooking id.
        /// </summary>
        public static BookingStatus GetBookingStatus(int bookingId)
        {
           var booking = _bookings.Find(b => b.BookingId == bookingId);
            return booking.Status;
        }

        /// <summary>
        /// Returns true if servicer provider exists in db.
        /// </summary>
        public static bool IsProviderExists(int providerId)
        {
            var provider = _providers.Find(b => b.Id == providerId);
            return provider != null;
        }

        /// <summary>
        /// Get the booking by unique booking id.
        /// </summary>
        public static BookingDetails GetBookingById(int id)
        {
            return _bookings.Find(b => b.BookingId == id);
        }

        /// <summary>
        /// Returns true if booking already exists.
        /// </summary>
        public static bool IsBookingExists(int bookingId)
        {
            var booking = _bookings.Find(b => b.BookingId == bookingId);
            return booking != null;
        }

        private static int GenerateUniqueId()
        {
            Random random = new Random();
            return random.Next();
        }
    }
}
