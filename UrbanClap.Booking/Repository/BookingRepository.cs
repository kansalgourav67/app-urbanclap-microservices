using System;
using System.Collections.Generic;
using System.Linq;

namespace UrbanClap.BookingService.Repository
{
    public class BookingRepository : IBookingRepository
    {
        public static List<Models.Booking> _bookings = new List<Models.Booking>();

        /// </inheritdoc>
        public int AddBooking(Models.Booking booking)
        {
            // create and set unique booking id.
            booking.BookingId = GenerateNewBookingId();
           _bookings.Add(booking);
            return booking.BookingId;
        }

        /// </inheritdoc>
        public Models.Booking GetBookingById(int bookingId)
        {
            return _bookings.FirstOrDefault(b => b.BookingId == bookingId);
        }

        /// </inheritdoc>
        public List<Models.Booking> GetAllBookingsDoneByCustomers(int customerId)
        {
            return _bookings.FindAll(b => b.CustomerId == customerId);
        }

        /// </inheritdoc>
        public int UpdateBooking(Models.Booking booking)
        {
            var bookingDetails = _bookings.Find(b => b.BookingId == booking.BookingId);

            bookingDetails.BookingStatus = booking.BookingStatus;
            bookingDetails.ServiceProviderId = booking.ServiceProviderId;
            return booking.BookingId;
        }

        /// </inheritdoc>
        public bool IsBookingExists(int bookingId)
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
