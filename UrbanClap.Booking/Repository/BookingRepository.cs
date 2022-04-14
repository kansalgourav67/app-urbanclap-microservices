using System;
using System.Collections.Generic;
using System.Linq;

namespace UrbanClap.BookingService.Repository
{
    public static class BookingRepository
    {
        public static List<Models.Booking> _bookings = new List<Models.Booking>();

        /// <summary>
        /// Creates new booking.
        /// </summary>
        public static int AddBooking(Models.Booking booking)
        {
            // create and set unique booking id.
            booking.BookingId = GenerateNewBookingId();
           _bookings.Add(booking);
            return booking.BookingId;
        }

        /// <summary>
        /// Gets booking as per provided booking id.
        /// </summary>
        public static Models.Booking GetBookingById(int bookingId)
        {
            return _bookings.FirstOrDefault(b => b.BookingId == bookingId);
        }

        /// <summary>
        /// Gets all bookings that are done by customer till date.
        /// </summary>
        public static List<Models.Booking> GetAllBookingsDoneByCustomers(int customerId)
        {
            return _bookings.FindAll(b => b.CustomerId == customerId);
        }

        /// <summary>
        /// Update the booking status and servicer provider.
        /// </summary>
        public static int UpdateBooking(Models.Booking booking)
        {
            var bookingDetails = _bookings.Find(b => b.BookingId == booking.BookingId);

            bookingDetails.BookingStatus = booking.BookingStatus;
            bookingDetails.ServiceProviderId = booking.ServiceProviderId;
            return booking.BookingId;
        }

        /// <summary>
        /// Returns true if the booking exists.
        /// </summary>
        public static bool IsBookingExists(int bookingId)
        {
            var booking = _bookings.Find(b => b.BookingId == bookingId);
            return booking != null;
        }

        private static int GenerateNewBookingId()
        {
            Random random = new Random();
            return random.Next();
        }
    }
}
