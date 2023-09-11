using Microsoft.AspNetCore.Mvc;
using ParkView.Services.IRepos;
using ParkView.Models;

namespace ParkView.Controllers
{
	public class HotelsController : Controller
	{
		private IHotelRepo _hotelRepo;

    public HotelsController(IHotelRepo hotelRepo)
    {
      _hotelRepo = hotelRepo;
    }

    [Route("Hotels")]
    public IActionResult Index()
		{
			return View();
		}

    [Route("Hotels/{location}")]
    public IActionResult HotelPage(string location)
		{
			Hotel hotel = _hotelRepo.GetHotelByLocation(location);

      return View(hotel);
		}
	}
}
