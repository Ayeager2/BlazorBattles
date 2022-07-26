﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBattle.Shared
{
    public class UserRegister : IUserRegister
    {
        [Required]
        public string Email { get; set; }
        [StringLength(16, ErrorMessage = "Your username is to long (16 characters max)")]
        public string UserName { get; set; }
        public string Bio { get; set; }
        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }
        public int StartUnitId { get; set; } = 1;
        [Range(0, 1000, ErrorMessage = "Please chose a number between 0 and 1000.")]
        public int Bananas { get; set; } = 100;
        public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;
        [Range(typeof(bool), "true", "true", ErrorMessage = "Please Confrim all Data is Correct!")]
        public bool IsConfirmed { get; set; }

    }
}

