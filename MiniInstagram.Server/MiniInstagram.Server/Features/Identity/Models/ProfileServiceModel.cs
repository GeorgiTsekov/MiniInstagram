using MiniInstagram.Server.Data.Models.Base;

namespace MiniInstagram.Server.Features.Identity.Models
{
    public class ProfileServiceModel
    {
        public string UserName { get; set; }

        public string ProfileUrl { get; set; }

        public string Gender { get; set; }

        public string WebSite { get; set; }

        public string Biography { get; set; }

        public bool IsPrivate { get; set; }
    }
}
