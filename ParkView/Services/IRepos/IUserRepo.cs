using ParkView.Models;

namespace ParkView.Services.IRepos
{
  public interface IUserRepo
  {
    public IEnumerable<AppUser> AllUsers {  get; }
    public AppUser GetUserById(string userEmail);
  }
}
