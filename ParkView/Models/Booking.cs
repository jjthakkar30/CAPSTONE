namespace ParkView.Models
{
	public class Booking
	{
		public int BookingId { get; set; }
		public string AppUserEmail { get; set; }
		public int HotelId { get; set; }
		public string? Status { get; set; }
		public DateTime CheckInDate { get; set; }
		public DateTime CheckOutDate { get; set; }
		public Rooms.Types? RoomType { get; set; }
		public int RoomCount { get; set; }
		public int ExtraBedsCount { get; set; }

		public bool IfCabSelected { get; set; }

		public int BillAmount { get; set; }
		public DateTime BookingDate { get; set; }
		public int CountAdult { get; set; }
		public int CountChildren_5To18 { get; set; }

		public Booking() { }
	}

	public static class BookingStatus
	{
		public const string Confirmed = "Confirmed";
		public const string Cart = "Cart";
		public const string Saved = "Saved";
		public const string Cancelled = "Cancelled";
	}
}
