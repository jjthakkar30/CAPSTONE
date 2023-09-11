using Microsoft.AspNetCore.Mvc;
using ParkView.Models;
using ParkView.Services.IRepos;
using System.Diagnostics;

namespace ParkView.Controllers
{
	public class HomeController : Controller
	{
		private IHotelRepo _hotelRepo;

    public HomeController(IHotelRepo hotelRepo)
    {
      _hotelRepo = hotelRepo;
    }

    public IActionResult Index()
		{
      return View(_hotelRepo.AllHotels);
		} 

		public IActionResult Services()
		{
			return View();
		}
		
		public IActionResult About()
		{
			return View();
		}
		
		public IActionResult Contact()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}