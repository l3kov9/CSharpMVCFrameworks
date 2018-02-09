using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CameraBazaar.Web.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z]{4,20}$", ErrorMessage = "Username must be between 4 and 20 symbols and must contains only letters.")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
