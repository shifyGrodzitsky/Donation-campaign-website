using ChineseSaleServer.DAL;
using ChineseSaleServer.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChineseSaleServer.BL
{
    public class AuthenticationService:IAuthenticationService
    {
        private readonly IUserDal _userDal;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUserDal userDal, IConfiguration configuration)
        {
            _userDal = userDal;
            _configuration = configuration;
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _userDal.LoginAsync(email, password);

            if (user == null)
            {
                return null; // Return null if user not found or password is incorrect
            }

            var token = GenerateToken(user); // Generate JWT token for the user
           
            return token+" "+user.Role;
        }

        public async Task Register(User user)
        {
            await _userDal.AddUserAsync(user);
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        }),
                Expires = DateTime.UtcNow.AddHours(24), // Set the token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        //private async Task <string> GenerateToken(User user)
        //{
        //    var issuer = _configuration["Jwt:Issuer"];
        //    var audience = _configuration["Jwt:Audience"];
        //    var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        //    var signingCredentials = new SigningCredentials(
        //                            new SymmetricSecurityKey(key),
        //                            SecurityAlgorithms.HmacSha256Signature
        //                        );
        //    var subject = new ClaimsIdentity(new[]
        //     {
        //                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString(), ClaimValueTypes.Integer),
        //                new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
        //                new Claim(JwtRegisteredClaimNames.Name, user.LastName),

        //                new Claim(JwtRegisteredClaimNames.Email, user.Email),

        //                new Claim("Phone", user.Phone.ToString()),
        //                 new Claim("Role", user.Role.ToString()),

        //            });

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = subject,
        //        Expires = null,
        //        Issuer = issuer,
        //        Audience = audience,
        //        SigningCredentials = signingCredentials
        //    };
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    var jwtToken = tokenHandler.WriteToken(token);

        //    return jwtToken;
        //}


    }
}
