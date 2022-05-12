using System.ComponentModel.DataAnnotations;

namespace MiniInstagram.Server.Features.Identity.Models
{
    public class RegisterUserRequestModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
