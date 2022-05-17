using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniInstagram.Server.Data;
using MiniInstagram.Server.Features.Identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniInstagram.Server.Features.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly MiniInstagramDbContext db;

        public IdentityService(MiniInstagramDbContext db)
        {
            this.db = db;
        }

        public string GenerateJwtToken(string userId, string email, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }

        public async Task<ProfileServiceModel> GetOne(string userId)
        {
            var profile = await this.db
                .Users
                .Where(u => u.Id == userId)
                .Select(u => new ProfileServiceModel
                {
                    UserName = u.UserName,
                    ProfileUrl = u.ProfileUrl,
                    Biography =  u.Biography,
                    Gender = u.Gender.ToString(),
                    IsPrivate = u.IsPrivate,
                    WebSite = u.WebSite,
                })
                .FirstOrDefaultAsync();

            return profile;
        }

        public bool IsEmailDublicated(string email)
        {
            return this.db.Users.Any(u => u.Email == email);
        }
    }
}
