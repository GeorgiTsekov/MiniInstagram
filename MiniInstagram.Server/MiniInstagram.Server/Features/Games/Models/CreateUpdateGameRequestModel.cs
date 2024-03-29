﻿using System.ComponentModel.DataAnnotations;
using static MiniInstagram.Server.Data.Validations.Game;

namespace MiniInstagram.Server.Features.Games.Models
{
    public class CreateUpdateGameRequestModel
    {
        [Required]
        [MaxLength(MAX_TITLE_LENGTH)]
        [MinLength(MIN_LENGTH)]
        public string Title { get; set; }

        [MaxLength(MAX_DESCRIPTION_LENGTH)]
        [MinLength(MIN_LENGTH)]
        public string Description { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }
    }
}
