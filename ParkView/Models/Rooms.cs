using System.Collections;

namespace ParkView.Models
{
	public static class Rooms
	{
		public static string[] TypeNames = { "Deluxe", "Super Deluxe", "Executive", "Presidential Suite" };
		// Data Format = 
		public static ArrayList RoomDetails = new()
		{
			new ArrayList() { "Deluxe", 150, 8000 },
			new ArrayList() { "Super Deluxe", 100, 14000 },
			new ArrayList() { "Executive", 50, 20000 },
			new ArrayList() { "Presidential Suite", 10, 28000 }
		};
		public enum Types
		{
			Deluxe,
			SuperDeluxe,
			Executive,
			PresidentialSuite
		}

		public static int[] MaxCapacity = { 150, 100, 50, 10 };
		public enum Capacity
		{
			Deluxe = 150,
			SuperDeluxe = 100,
			Executive = 50,
			PresidentialSuite = 10
		};


		public enum Price
		{
			Deluxe = 8000,
			SuperDeluxe = 14000,
			Executive = 20000,
			PresidentialSuite = 28000
		}
	}
}
