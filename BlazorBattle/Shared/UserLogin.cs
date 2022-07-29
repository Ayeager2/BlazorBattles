using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBattle.Shared
{
    public class UserLogin
    {
        [Required (ErrorMessage = "Please enter a UserName.")]

        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter a Password.")]

        public string Password { get; set; }
    }
}
