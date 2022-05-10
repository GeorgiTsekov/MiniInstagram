using Microsoft.AspNetCore.Identity;

namespace MiniInstagram.Server.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Games = new List<Game>();
        }

        public IEnumerable<Game> Games { get; private set; }
    }
}
