using ServiceProvider.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanClap.Common.EventBus.Messages;

namespace UrbanClap.ServiceProvider.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        public static List<BookingDetails> _bookings = new List<BookingDetails>();

        /// </inheritdoc>
        public int AddBooking(BookingDetails bookingDetails)
        {
            _bookings.Add(bookingDetails);
            return bookingDetails.BookingId;
        }

        /// </inheritdoc>
        public List<BookingDetails> GetBookingsByProviderId(int providerId)
        {
            return _bookings.FindAll(b => b.ServiceProviderId == providerId && b.BookingStatus == BookingStatus.Received);
        }

        /// </inheritdoc>
        public int UpdateBookingStatus(BookingDetails bookingDetails)
        {
            var booking = _bookings.Find(b => b.BookingId == bookingDetails.BookingId);
            booking.BookingStatus = bookingDetails.BookingStatus;
            return bookingDetails.BookingId;
        }

        /// </inheritdoc>
        public BookingStatus GetBookingStatus(int bookingId)
        {
            var booking = _bookings.Find(b => b.BookingId == bookingId);
            return booking.BookingStatus;
        }

        /// </inheritdoc>
        public BookingDetails GetBookingById(int id)
        {
            return _bookings.Find(b => b.BookingId == id);
        }

        /// </inheritdoc>
        public bool IsBookingExists(int bookingId)
        {
            var booking = _bookings.Find(b => b.BookingId == bookingId);
            return booking != null;
        }
    }
}
