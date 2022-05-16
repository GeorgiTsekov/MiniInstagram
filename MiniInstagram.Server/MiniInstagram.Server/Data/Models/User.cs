using Microsoft.AspNetCore.Identity;
using MiniInstagram.Server.Data.Models.Base;

namespace MiniInstagram.Server.Data.Models
{
    public class User : IdentityUser, IEntity
    {
        public User()
        {
            this.Games = new List<Game>();
        }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }

        public IEnumerable<Game> Games { get; private set; }
    }
}
