using Microsoft.AspNetCore.Identity;
using MiniInstagram.Server.Data.Models.Base;
using System.ComponentModel.DataAnnotations;
using static MiniInstagram.Server.Data.Validations.User;

namespace MiniInstagram.Server.Data.Models
{
    public class User : IdentityUser, IEntity
    {
        public User()
        {
            this.Games = new HashSet<Game>();
        }

        public string ProfileUrl { get; set; }

        public Gender Gender { get; set; }

        public string WebSite { get; set; }


        [MaxLength(MAX_BIOGRAPHY_LENGTH)]
        public string Biography { get; set; }

        public bool IsPrivate { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }

        public IEnumerable<Game> Games { get; private set; }
    }
}
