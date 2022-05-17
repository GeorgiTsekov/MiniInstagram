using System.ComponentModel.DataAnnotations;

namespace MiniInstagram.Server.Features.Identity.Models
{
    public class LoginRequestModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
