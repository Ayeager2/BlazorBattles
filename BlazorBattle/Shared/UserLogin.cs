using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBattle.Shared
{
    public class UserLogin : IUserLogin
    {
        [Required(ErrorMessage = "Please enter an email address.")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a Password.")]

        public string Password { get; set; }
    }
}
