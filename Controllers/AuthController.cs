using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkSphereAPI.Data;
using WorkSphereAPI.Helpers;
using WorkSphereAPI.Models;
using WorkSphereAPI.DTO;

namespace WorkSphereAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly JwtSettings _jwtSettings;

        public AuthController(ApplicationDbContext db, IOptions<JwtSettings> jwtSettings)
        {
            _db = db;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] WorkSphereAPI.DTO.LoginRequest request)
        {
            var user = _db.Users.SingleOrDefault(u => u.Username == request.Username);

            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
                return Unauthorized("Invalid username or password.");

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        private bool VerifyPassword(string rawPassword, string hashed)
        {
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(rawPassword));
            return base64 == hashed;
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
{
    new Claim(ClaimTypes.Name, user.Username),
    new Claim(ClaimTypes.Role, user.Role),
    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // ✅ Add this line
};


            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
