using System;
using System.Collections.Generic;

namespace UrbanClap.AdministrationService.Repositories
{
    public interface IBookingRepository
    {
        List<Booking> GetAllRecentBookings();

        bool IsBookingExists(int bookingId);

        int AddBooking(Booking booking);

        Booking GetBookingById(int id);

        int UpdateBooking(Booking booking);
    }
}
