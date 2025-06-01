using LibraryWebApi.Models;
using LibraryWebApi.Repo.Abstract;
using LibraryWebApi.RoleServices;
using LibraryWebApi.Service.Abstract;

namespace LibraryWebApi.Service.Concrete
{
    public class UserService : IUserService
    {
        private readonly IRepo<User> repo;
        public UserService(IRepo<User> repo)
        {
            this.repo = repo;
        }
        public User AuthenticateUser(User user)
        {
            return repo.Get(x => x.UserName == user.UserName && x.Password == user.Password);
        }

        //Bir şekilde UserName in unique olmasini saglamaliyim, [Key] olabilir
        public User GetUserByName(string username)
        {
            return repo.Get(x => x.UserName == username);
        }

        public void AddUser(User user)
        {
            //Bu kısım ve diğer role kısımları frameworkle degistirilebilir
            user._UserRole = new UserRole { Role =  Roles.User };
            repo.Add(user);
        }

        //userManager List<string> donduruyor, benimkinde tek role oldugu icin string donduruyor
        public string GetUserRole(User user)
        {
            return user._UserRole.ToString();
        }
    }
}
