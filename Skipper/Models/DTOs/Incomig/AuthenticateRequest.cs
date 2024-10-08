﻿using System.ComponentModel.DataAnnotations;

namespace Skipper.Models.DTOs.Incomig
{
    public class AuthenticateRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(8)]
        public string Password { get; set; } = string.Empty;
    }
}
