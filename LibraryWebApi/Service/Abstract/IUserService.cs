using LibraryWebApi.Models;
using LibraryWebApi.RoleServices;

namespace LibraryWebApi.Service.Abstract
{
    public interface IUserService
    {
        User AuthenticateUser(User user);
        User GetUserByName(string username);
        void AddUser(User user);

        string GetUserRole(User user);
    }
}
