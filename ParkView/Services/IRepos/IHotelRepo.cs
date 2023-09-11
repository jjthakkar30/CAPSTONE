using ParkView.Models;

namespace ParkView.Services.IRepos
{
  public interface IHotelRepo
  {
    public IEnumerable<Hotel> AllHotels { get; }
    public Hotel GetHotelById(int hotelId);
    public Hotel GetHotelByLocation(string hotelLocation);
  }
}
