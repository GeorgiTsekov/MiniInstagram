using MiniInstagram.Server.Data.Models.Base;
using MiniInstagram.Server.Features.Identity.Models;
using MiniInstagram.Server.Infrastructure.Services;

namespace MiniInstagram.Server.Features.Identity
{
    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string userName, string secret);

        Task<Result> IsEmailUnique(string email);

        Task<ProfileServiceModel> GetOne(string userId);

        Task<Result> Update(
            string userId, 
            string profileUrl, 
            Gender gender, 
            string webSite, 
            string biography, 
            bool isPrivate);

        Task<IEnumerable<SearchUsersServiceModel>> SearchByUserName(string query);
    }
}
