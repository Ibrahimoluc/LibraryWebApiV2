using LibraryWebApi.Models;
using LibraryWebApi.Service.Abstract;
using Microsoft.AspNetCore.Identity;

namespace LibraryWebApi.Service.Concrete
{
    public class PasswordHelper : IPasswordHelper
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        public PasswordHelper(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }


        //Doğru ya da yanlış döndüren genel bir fonksiyon
        public bool VerifyPassword(User user, string hashedPassword, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, password);
            // if required, you can handle if result is SuccessRehashNeeded
            return result == PasswordVerificationResult.Success;
        }
    }
}
