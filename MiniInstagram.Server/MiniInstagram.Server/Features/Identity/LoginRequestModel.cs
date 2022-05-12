﻿using System.ComponentModel.DataAnnotations;

namespace MiniInstagram.Server.Features.Identity
{
    public class LoginRequestModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}