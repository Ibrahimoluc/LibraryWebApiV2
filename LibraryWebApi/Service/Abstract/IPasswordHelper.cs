using LibraryWebApi.Models;

namespace LibraryWebApi.Service.Abstract
{
    public interface IPasswordHelper
    {
        string HashPassword(User user, string password);
        bool VerifyPassword(User user, string hashedPassword, string password);
    }
}
