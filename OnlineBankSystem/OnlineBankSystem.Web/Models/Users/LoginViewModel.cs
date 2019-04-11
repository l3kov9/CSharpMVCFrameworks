namespace OnlineBankSystem.Web.Models.Users
{
    using Infrastructure.Validations;
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username can't be empty")]
        [MinLength(5, ErrorMessage = "Username must be between 5 and 50 symbols")]
        [MaxLength(50, ErrorMessage = "Username must be between 5 and 50 symbols")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password can't be empty")]
        [Password]
        public string Password { get; set; }

        [Display(Name = "I'm not a robor")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Please verify that you are not a robot")]
        public bool IsReal { get; set; }
    }
}