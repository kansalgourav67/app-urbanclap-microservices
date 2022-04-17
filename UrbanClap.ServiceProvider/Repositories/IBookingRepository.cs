using ServiceProvider.Repository;
using System;
using System.Collections.Generic;
using UrbanClap.Common.EventBus.Messages;

namespace UrbanClap.ServiceProvider.Repositories
{
    public interface IBookingRepository
    {
        /// <summary>
        /// Add new booking.
        /// </summary>
        int AddBooking(BookingDetails bookingDetails);

        /// <summary>
        /// Returns all bookings that are allocated to service provider.
        /// </summary>
        List<BookingDetails> GetBookingsByProviderId(int providerId);

        /// <summary>
        /// Update the booking status.
        /// </summary>
        int UpdateBookingStatus(BookingDetails bookingDetails);

        /// <summary>
        /// Gets the booking status by boooking id.
        /// </summary>
        BookingStatus GetBookingStatus(int bookingId);

        /// <summary>
        /// Get the booking by unique booking id.
        /// </summary>
        BookingDetails GetBookingById(int id);

        /// <summary>
        /// Returns true if booking already exists.
        /// </summary>
        bool IsBookingExists(int bookingId);
    }
}
