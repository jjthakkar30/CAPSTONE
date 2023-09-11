using ParkView.Models;

namespace ParkView.ViewModels
{
  public class BookingViewModel
  {
    public Booking _booking;
    public AppUser _user;
    public string TransactionId;

    public BookingViewModel()
    {
      _booking = new();
      _user = new();
    }
  }
}
