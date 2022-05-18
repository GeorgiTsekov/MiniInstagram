using MiniInstagram.Server.Data.Models.Base;
using System.ComponentModel.DataAnnotations;
using static MiniInstagram.Server.Data.Validations.User;

namespace MiniInstagram.Server.Features.Identity.Models
{
    public class UpdateProfileRequestModel
    {
        public string ProfileUrl { get; set; }

        public Gender Gender { get; set; }

        public string WebSite { get; set; }


        [MaxLength(MAX_BIOGRAPHY_LENGTH)]
        public string Biography { get; set; }

        public bool IsPrivate { get; set; }
    }
}
