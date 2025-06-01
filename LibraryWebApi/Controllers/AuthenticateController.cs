using System.Diagnostics;
using LibraryWebApi.DTOs;
using LibraryWebApi.Models;
using LibraryWebApi.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        //appsettings.json dan bilgileri alıyor, otomatik olarak da DI yapılıyor sanırım?
        private readonly IConfiguration config;
        private readonly IUserService userService;
        private readonly IJwtService jwtService;
        private readonly IPasswordHelper passwordHelper;
        public AuthenticateController(IConfiguration config, IUserService userService, IJwtService jwtService, IPasswordHelper passwordHelper) 
        {
            this.config = config;
            this.userService = userService;
            this.jwtService = jwtService;
            this.passwordHelper = passwordHelper;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO request)
        {
            Console.WriteLine(request.UserName);

            var user = userService.GetUserByName(request.UserName);
            if (user == null)
            {
                Console.WriteLine("User bulunamadı");
                return NotFound("User not found");
            }

            if (passwordHelper.VerifyPassword(user, user.Password, request.Password))
            {
                var tokenStr = jwtService.GenerateJwt(user);
                return Ok(new { token = tokenStr });
            }

            return NotFound("UserName veya Password yanlış");
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO request)
        {
            Console.WriteLine($"request.UserName:{request.UserName}, request.Password:{request.Password}");

            var user = new User { UserName = request.UserName};
            string hashedPass = passwordHelper.HashPassword(user, request.Password);
            Console.WriteLine("hashedPass:" + hashedPass);
            user.Password = hashedPass;
            userService.AddUser(user);

            return Ok(user);
        }

    }
}
