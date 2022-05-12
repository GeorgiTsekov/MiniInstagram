using System.ComponentModel.DataAnnotations;
using static MiniInstagram.Server.Data.Validations.Game;

namespace MiniInstagram.Server.Features.Games.Models
{
    public class CreateGameRequestModel
    {
        [Required]
        [MaxLength(MAX_TITLE_LENGTH)]
        public string Title { get; set; }

        [MaxLength(MAX_DESCRIPTION_LENGTH)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
