using System.ComponentModel.DataAnnotations;
using static MiniInstagram.Server.Helpers.Validations.Game;

namespace MiniInstagram.Server.Models.Games
{
    public class CreateGamesRequestModel
    {
        [Required]
        [MaxLength(MAX_TITLE_LENGTH)]
        public string Title { get; set; }

        [Required]
        [MaxLength(MAX_DESCRIPTION_LENGTH)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
