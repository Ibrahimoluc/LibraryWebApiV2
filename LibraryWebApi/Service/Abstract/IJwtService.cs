using LibraryWebApi.Models;

namespace LibraryWebApi.Service.Abstract
{
    public interface IJwtService
    {
        string GenerateJwt(User user);
    }
}
