using ParkView.Models;
using ParkView.Services.IRepos;

namespace ParkView.Services
{
  public class ReviewDbRepo : IReviewRepo
  {
    private ApplicationDbContext _context;

    public ReviewDbRepo(ApplicationDbContext context)
    {
      _context = context;
    }

    public Review GetReviewByReviewId(int reviewId)
    {
      return _context.Reviews.FirstOrDefault(review => review.ReviewId == reviewId);
    }

		public IEnumerable<Review> GetReviewsByHotelId(int hotelId)
    {
      return from review in _context.Reviews
             where review.HotelId == hotelId
             select review;
    }


		public IEnumerable<Review> GetReviewsByUserId(string userEmail)
    {
      return from review in _context.Reviews
             where review.AppUserEmail == userEmail
             select review;
    }
	}
}
