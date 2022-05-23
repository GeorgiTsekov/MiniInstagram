using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniInstagram.Server.Data;
using MiniInstagram.Server.Data.Models;
using MiniInstagram.Server.Data.Models.Base;
using MiniInstagram.Server.Features.Identity.Models;
using MiniInstagram.Server.Infrastructure.Services;
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

        public async Task<IEnumerable<SearchUsersServiceModel>> SearchByUserName(string query)
        {
            return await this.db
                .Users
                .Where(u => u.UserName.ToLower().Contains(query.ToLower()))
                .Select(u => new SearchUsersServiceModel
                {
                    UserName = u.UserName,
                    ProfileUrl = u.ProfileUrl,
                    UserId = u.Id,
                })
                .ToListAsync();
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

        public async Task<Result> IsEmailUnique(string email)
        {
            var user = await this.db.Users.AnyAsync(u => u.Email == email);
            if (user)
            {
                return $"Email: {email} is dublicated";
            }

            return true;
        }

        public async Task<Result> Update(
            string userId, 
            string profileUrl, 
            Gender gender, 
            string webSite, 
            string biography, 
            bool isPrivate)
        {
            var user = await this.db
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return "You are not authorized to update this user";
            }

            this.ChangeUserProfile(profileUrl, gender, webSite, biography, isPrivate, user);

            await this.db.SaveChangesAsync();

            return true;
        }

        private void ChangeUserProfile(string profileUrl, Gender gender, string webSite, string biography, bool isPrivate, User user)
        {
            if (user.ProfileUrl != profileUrl)
            {
                user.ProfileUrl = profileUrl;
            }

            if (user.Gender != gender)
            {
                user.Gender = gender;
            }

            if (user.WebSite != webSite)
            {
                user.WebSite = webSite;
            }

            if (user.Biography != biography)
            {
                user.Biography = biography;
            }

            if (user.IsPrivate != isPrivate)
            {
                user.IsPrivate = isPrivate;
            }
        }
    }
}
