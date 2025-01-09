﻿using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem.API.Models
{
    public class LoginRequest
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; } 
    }
}