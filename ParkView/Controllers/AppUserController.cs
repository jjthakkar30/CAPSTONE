using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ParkView.Models;
using ParkView.Services;
using ParkView.Services.IRepos;
using System.Security.Claims;

namespace ParkView.Controllers
{
  public class AppUserController : Controller
  {
	private IUserRepo _userRepo;
	private IBookingRepo _bookingRepo;
		private ApplicationDbContext _context;
		private IHotelRepo _hotelRepo;

		public AppUserController(IUserRepo userRepo, IBookingRepo bookingRepo, ApplicationDbContext context, IHotelRepo hotelRepo)
		{
			_userRepo = userRepo;
			_bookingRepo = bookingRepo;
			_context = context;
			_hotelRepo = hotelRepo;
		}

	[Route("user/{userid}/cart")]
	public IActionResult Cart(int id)
	{
      var claimIdentity = (ClaimsIdentity)User.Identity;
      var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
      AppUser user = (AppUser)_context.Users.FirstOrDefault(u => u.Id == userId);
			IEnumerable<Booking> userCart= _bookingRepo.GetUserCart(user.Email);
			return View(userCart);
	}

    [HttpPost]
    public IActionResult AddToCart(Booking booking)
		{
			booking.Status = BookingStatus.Cart;
			_context.Add(booking);
			_context.SaveChanges();
			HomeController hc = new(_hotelRepo);
			return hc.Index();
		}

    [HttpPost]
    public IActionResult SaveForLater(Booking booking)
		{
			booking.Status = BookingStatus.Saved;
			_context.Add(booking);
			_context.SaveChanges();
      HomeController hc = new(_hotelRepo);
			return hc.Index();
    }

	[Route("user/{userid}/bookings")]
	public IActionResult UserBookings(int id)
	{
      var claimIdentity = (ClaimsIdentity)User.Identity;
      var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
      AppUser user = (AppUser)_context.Users.FirstOrDefault(u => u.Id == userId);
      IEnumerable<Booking> userBookings = _bookingRepo.GetBookingsByUserId(user.Email);
      return View(userBookings);
	}
}
}
