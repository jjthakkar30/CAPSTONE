using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using ParkView.Services.IRepos;
using ParkView.Services;
using ParkView.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ParkView
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection");
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
					options.UseSqlServer(connectionString));

               builder.Services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
			builder.Services.AddDatabaseDeveloperPageExceptionFilter();

			//builder.Services.AddScoped<IDbInitializer, DbInitializer>();
			builder.Services.AddScoped<IEmailSender, EmailSender>();
			builder.Services.AddScoped<IHotelRepo, HotelDbRepo>();
			builder.Services.AddScoped<IUserRepo, UserDbRepo>();
			builder.Services.AddScoped<IBookingRepo, BookingDbRepo>();
			builder.Services.AddScoped<IReviewRepo, ReviewDbRepo>();

			builder.Services.AddRazorPages();

			builder.Services.AddControllersWithViews();

      builder.Services.AddHttpContextAccessor();
      builder.Services.AddSession();
      builder.Services.AddAuthentication().AddGoogle(
          GoogleOptions =>
          {
            GoogleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
            GoogleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
          });
      builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest)
         .AddRazorPagesOptions(options =>
         {
           options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
           options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
         });



      builder.Services.ConfigureApplicationCookie(options =>
      {
        options.LoginPath = $"/Identity/Account/Login";
        options.LogoutPath = $"/Identity/Account/Logout";
        options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
      });

      var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			//using (var scope = app.Services.CreateScope())
			//{
			//	IDbInitializer dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
			//	dbInitializer.Initialize();
			//}

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			app.MapRazorPages();

			app.Run();
		}
	}
}