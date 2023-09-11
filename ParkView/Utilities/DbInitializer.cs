using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ParkView.Models;
using ParkView.Services;

namespace ParkView.Utilities
{
	public class DbInitializer : IDbInitializer
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ApplicationDbContext _context;

    public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
    {
			_userManager = userManager;
			_roleManager = roleManager;
			_context = context;
    }

		public void Initialize()
		{
			try
			{
				if (_context.Database.GetPendingMigrations().Count() > 0)
				{
					_context.Database.Migrate();
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			if(!_roleManager.RoleExistsAsync(WebSiteRoles.Hotel_Admin).GetAwaiter().GetResult())
			{
				_roleManager.CreateAsync(new IdentityRole(WebSiteRoles.Hotel_Admin)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(WebSiteRoles.Hotel_Customer)).GetAwaiter().GetResult();

				_userManager.CreateAsync(new AppUser
				{
					FirstName = "Admin",
					Email = "admin@gmail.com",
					UserName = "admin@gmail.com"
				}, "Admin123");
				AppUser user = _context.AppUsers.FirstOrDefault(x => x.Email == "admin@gmail.com");
				_userManager.AddToRoleAsync(user, WebSiteRoles.Hotel_Admin);
			}
		}
	}
}
