using Microsoft.AspNetCore.Identity;
using ParkView.Models;
using ParkView.Services.IRepos;

namespace ParkView.Services
{
  public class UserDbRepo : IUserRepo
  {
    private ApplicationDbContext _context;

    public UserDbRepo(ApplicationDbContext context)
    {
      _context = context;
    }

    public IEnumerable<AppUser> AllUsers
    {
      get
      {
        return _context.AppUsers.ToList();
      }
    }

    public AppUser GetUserById(string userEmail)
    {
      return _context.AppUsers.FirstOrDefault(user => user.UserName == userEmail);
    }
	}
}
