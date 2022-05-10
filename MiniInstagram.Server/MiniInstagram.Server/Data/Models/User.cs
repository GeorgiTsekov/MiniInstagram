using Microsoft.AspNetCore.Identity;

namespace MiniInstagram.Server.Data.Models
{
    public class User : IdentityUser
    {
        public IEnumerable<Game> Games { get; private set; } = new HashSet<Game>();
    }
}
