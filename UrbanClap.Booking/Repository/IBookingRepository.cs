using System;
using System.Collections.Generic;

namespace UrbanClap.BookingService.Repository
{
    public interface IBookingRepository
    {
        /// <summary>
        /// Creates new booking.
        /// </summary>
        int AddBooking(Models.Booking booking);

        /// <summary>
        /// Gets booking as per provided booking id.
        /// </summary>
        Models.Booking GetBookingById(int bookingId);

        /// <summary>
        /// Gets all bookings that are done by customer till date.
        /// </summary>
        List<Models.Booking> GetAllBookingsDoneByCustomers(int customerId);

        /// <summary>
        /// Update the booking status and servicer provider.
        /// </summary>
        int UpdateBooking(Models.Booking booking);

        /// <summary>
        /// Returns true if the booking exists.
        /// </summary>
        bool IsBookingExists(int bookingId);
    }
}
