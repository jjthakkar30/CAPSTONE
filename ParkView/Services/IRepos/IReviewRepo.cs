using ParkView.Models;

namespace ParkView.Services.IRepos
{
  public interface IReviewRepo
  {
    public IEnumerable<Review> GetReviewsByHotelId(int hotelId);
    public IEnumerable<Review> GetReviewsByUserId(string userEmail);
    public Review GetReviewByReviewId(int reviewId);
  }
}
