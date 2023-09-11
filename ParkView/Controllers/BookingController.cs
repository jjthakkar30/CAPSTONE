using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkView.Models;
using ParkView.Services;
using ParkView.Services.IRepos;
using ParkView.ViewModels;
using Razorpay.Api;
using System.Security.Claims;

namespace ParkView.Controllers
{
  [Authorize]
    public class BookingController : Controller
    {
        private IBookingRepo _bookingRepo;
        private ApplicationDbContext _context;

        [BindProperty]
        public BookingViewModel viewModel { get; set; }

        public BookingController(IBookingRepo bookingRepo, ApplicationDbContext context)
        {
            _bookingRepo = bookingRepo;
            _context = context;
            viewModel = new();
        }

        [Route("Booking")]
        public ActionResult BookingForm()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            AppUser user = (AppUser)_context.Users.FirstOrDefault(u => u.Id == userId);
            viewModel._user = user;
            return View(viewModel);
        }

        public IActionResult InitiateBookingPayment()
        {
            string key = "rzp_test_09cYvlbueZYlpL";
            string secret = "5oMusm1tF8nBGFtxUcPLCYaH";

            Random _random = new Random();
            string TransactionId = _random.Next(0, 10000).ToString();

            RazorpayClient client = new RazorpayClient(key, secret);
            viewModel._booking.BillAmount = 10;
            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", Convert.ToDecimal(viewModel._booking.BillAmount)*100); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", TransactionId);

            Razorpay.Api.Order order = client.Order.Create(input);
            ViewBag.orderId = order["id"].ToString();

            return View("Payment", viewModel);
        }

        [HttpPost] 
        [Route("Booking/Success")]
        public ActionResult Sucess(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature)
        {
            Dictionary<string, string> attributes = new();
            attributes.Add("razorpay_payment_id", razorpay_payment_id);
            attributes.Add("razorpay_order_id", razorpay_order_id);
            attributes.Add("razorpay_signature", razorpay_signature);

            Utils.verifyPaymentSignature(attributes);

            BookingViewModel _viewModel = new()
            {
              TransactionId = razorpay_payment_id,
            };
            
            _viewModel._booking.BookingDate = DateTime.Now;
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            AppUser user = (AppUser)_context.Users.FirstOrDefault(u => u.Id == userId);
            _viewModel._booking.AppUserEmail = user.Email;
            _bookingRepo.ConfirmBooking(_viewModel._booking);
            return View("Success", _viewModel);
        }

        public IActionResult ConfirmBooking()
        {
            return View(_bookingRepo.AllBookings);
        }

        public int GenerateBillAmount(Booking booking)
        {
            return 0;
        }

        public bool ValidateBooking(Booking booking)
        {
            IEnumerable<Booking> hotelBookings = _bookingRepo.GetBookingsByHotelId(booking.HotelId);
            int bookedRooms = 0;
            foreach(Booking b in hotelBookings)
            {
                if (b.RoomType == booking.RoomType && (
                    (booking.CheckInDate >= b.CheckInDate && booking.CheckInDate <= b.CheckOutDate) ||
                    (booking.CheckInDate >= b.CheckInDate && booking.CheckInDate <= b.CheckOutDate))
                )
                {
                    bookedRooms += b.RoomCount;
                }
            }

            return booking.RoomCount <= Rooms.MaxCapacity[(int)booking.RoomType] - bookedRooms;
        }

    }
}
