using ParkView.Models;
using ParkView.Services;
using ParkView.Services.IRepos;

namespace ParkView.Services
{
  public class HotelDbRepo : IHotelRepo
  {
    ApplicationDbContext _context;

    public HotelDbRepo(ApplicationDbContext context)
    {
      _context = context;
    }

    public IEnumerable<Hotel> AllHotels
    {
      get
      {
        return _context.Hotels.ToList();
      }
    }

    public Hotel GetHotelById(int hotelId)
    {
      return _context.Hotels.FirstOrDefault(hotel => hotel.HotelId == hotelId);
    }

    public Hotel GetHotelByLocation(string hotelLocation)
    {
      return _context.Hotels.FirstOrDefault(hotel => hotel.Location == hotelLocation);
    }
  }
}
