using MiniInstagram.Server.Features.Identity.Models;

namespace MiniInstagram.Server.Features.Identity
{
    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string userName, string secret);

        bool IsEmailDublicated(string email);

        Task<ProfileServiceModel> GetOne(string userId);
    }
}
