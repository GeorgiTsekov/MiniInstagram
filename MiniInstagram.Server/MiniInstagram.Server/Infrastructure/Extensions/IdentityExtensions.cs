using System.Security.Claims;

namespace MiniInstagram.Server.Infrastructure.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user
                .Claims
                .FirstOrDefault(g => g.Type == ClaimTypes.NameIdentifier)
                ?.Value;
        }

        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user
                .Claims
                .FirstOrDefault(g => g.Type == ClaimTypes.Email)
                ?.Value;
        }
    }
}
