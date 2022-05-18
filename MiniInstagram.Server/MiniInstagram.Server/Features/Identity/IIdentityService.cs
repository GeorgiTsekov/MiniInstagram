using MiniInstagram.Server.Data.Models.Base;
using MiniInstagram.Server.Features.Identity.Models;

namespace MiniInstagram.Server.Features.Identity
{
    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string userName, string secret);

        bool IsEmailDublicated(string email);

        Task<ProfileServiceModel> GetOne(string userId);

        Task<bool> Update(
            string userId, 
            string profileUrl, 
            Gender gender, 
            string webSite, 
            string biography, 
            bool isPrivate);
    }
}
