using MiniInstagram.Server.Data.Models.Base;
using System.ComponentModel.DataAnnotations;
using static MiniInstagram.Server.Data.Validations.User;

namespace MiniInstagram.Server.Features.Identity.Models
{
    public class UpdateProfileRequestModel
    {
        [Url]
        public string ProfileUrl { get; set; }

        [Range(0, 2)]
        public Gender Gender { get; set; }

        [Url]
        public string WebSite { get; set; }

        [MaxLength(MAX_BIOGRAPHY_LENGTH)]
        [MinLength(MIN_LENGTH)]
        public string Biography { get; set; }

        public bool IsPrivate { get; set; }
    }
}
