using Microsoft.AspNetCore.Identity;

namespace ParkView.Models
{
	public class AppUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string? LastName { get; set; }

		public AppUser() { }
	}
}
