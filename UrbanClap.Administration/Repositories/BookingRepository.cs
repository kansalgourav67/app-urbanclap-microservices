using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanClap.Common.EventBus.Messages;

namespace UrbanClap.AdministrationService.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private static List<Booking> _bookings = new List<Booking>();

        /// <summary>
        /// Register booking in the database.
        /// </summary>
        public int AddBooking(Booking booking)
        {
            _bookings.Add(booking);
            return booking.BookingId;
        }

        /// <summary>
        /// Returns all recent bookings that are having status received.
        /// </summary>
        public List<Booking> GetAllRecentBookings()
        {
            return _bookings.FindAll(b => b.BookingStatus == BookingStatus.Received);
        }

        /// <summary>
        /// Get the booking by booking id.
        /// </summary>
        public Booking GetBookingById(int id)
        {
            return _bookings.Find(b => b.BookingId == id);
        }

        /// <summary>
        /// Returns true if booking already exists.
        /// </summary>
        public bool IsBookingExists(int bookingId)
        {
            var booking = _bookings.Find(b => b.BookingId == bookingId);
            return booking != null;
        }

        /// <summary>
        /// Updates the booking status and service provider.
        /// </summary>
        public int UpdateBooking(Booking bookingToUpdate)
        {
            var booking = _bookings.Find(b => b.BookingId == bookingToUpdate.BookingId);
            booking.BookingStatus = bookingToUpdate.BookingStatus;
            booking.ServiceProviderId = bookingToUpdate.ServiceProviderId;
            return booking.BookingId;
        }
    }
}
