using MiniInstagram.Server.Infrastructure.Extensions;
using System.Security.Claims;

namespace MiniInstagram.Server.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly ClaimsPrincipal user;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.user = httpContextAccessor.HttpContext?.User;
        }

        public string GetId()
        {
            var userId = this.user?.GetId();

            return userId;
        }

        public string GetUserName()
        {
            var userName = this.user?.GetUserName();

            return userName;
        }
    }
}
