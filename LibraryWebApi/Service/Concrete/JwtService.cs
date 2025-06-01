using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibraryWebApi.Models;
using LibraryWebApi.Service.Abstract;
using Microsoft.IdentityModel.Tokens;

namespace LibraryWebApi.Service.Concrete
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration config;
        private readonly IUserService userService;

        public JwtService(IConfiguration config, IUserService userService)
        {
            this.config = config;
            this.userService = userService; 
        }
        public string GenerateJwt(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            string? userRole = userService.GetUserRole(user);
            Console.WriteLine("Tokene eklenecek userRole:" + userRole);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),

        new Claim(JwtRegisteredClaimNames.Iss, config["Jwt:Issuer"]),
        new Claim(JwtRegisteredClaimNames.Aud, config["Jwt:Audience"]),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

        //role ekleme, gelen userdan cekilebilir Role degeri
        //onemli nokta ClaimTypes.Role olabilir duzgun calismasi icin, pek de onemli degil gibi
        //onun yerine "role" yaptıgımda da calıştı
        //sacma bir sey yapsam, name gibi
        new Claim(ClaimTypes.Role, userRole)
    };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
