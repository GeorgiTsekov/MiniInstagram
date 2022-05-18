using System.ComponentModel.DataAnnotations;
using static MiniInstagram.Server.Data.Validations.User;

namespace MiniInstagram.Server.Features.Identity.Models
{
    public class RegisterUserRequestModel
    {
        [Required]
        [MinLength(MIN_LENGTH)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
