namespace ParkView.Models
{
	public class Review
	{
		public int ReviewId { get; set; }
		public int HotelId { get; set; }
		public string AppUserEmail { get; set; }
		public string? Feedback { get; set; }
		public int Rating { get; set; }
	}
}
