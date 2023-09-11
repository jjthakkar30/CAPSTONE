using ParkView.Models;
using ParkView.Services;
using ParkView.Services.IRepos;

namespace ParkView.Services
{
  public class BookingDbRepo : IBookingRepo
  {
    private ApplicationDbContext _context;

    public BookingDbRepo(ApplicationDbContext context)
    {
      _context = context;
    }

    public Booking GetBookingByBookingId(int BookingId)
    {
      return _context.Bookings.FirstOrDefault(booking => booking.BookingId == BookingId);
    }

    public IEnumerable<Booking> GetBookingsByHotelId(int HotelId)
    {
      return from booking in _context.Bookings
             where booking.HotelId == HotelId
             select booking;
    }

    public IEnumerable<Booking> GetBookingsByUserId(string userEmail)
    {
      return from booking in _context.Bookings
             where (booking.AppUserEmail == userEmail && booking.Status != "Cart")
             select booking;
    }
    
    public IEnumerable<Booking> GetUserCart(string userEmail)
    {
      return from booking in _context.Bookings
             where (booking.AppUserEmail == userEmail && booking.Status == "Cart")
             select booking;
    }

		public void ConfirmBooking(Booking booking)
		{
		  _context.Bookings.Add(booking);
      _context.SaveChanges();
		}

		public IEnumerable<Booking> AllBookings
    {
      get => _context.Bookings.ToList();
    }
  }
}
