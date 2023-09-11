using ParkView.Models;

namespace ParkView.Services.IRepos
{
  public interface IBookingRepo
  {
    public Booking GetBookingByBookingId(int BookingId);
    public IEnumerable<Booking> GetBookingsByUserId(string userEmail);
    public IEnumerable<Booking> GetBookingsByHotelId(int HotelId);
    public IEnumerable<Booking> GetUserCart(string userEmail);
    public void ConfirmBooking(Booking booking);

		public IEnumerable<Booking> AllBookings { get; }
  }
}
