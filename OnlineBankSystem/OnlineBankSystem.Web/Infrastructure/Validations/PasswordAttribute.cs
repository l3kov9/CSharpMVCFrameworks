namespace OnlineBankSystem.Web.Infrastructure.Validations
{
    using System.ComponentModel.DataAnnotations;

    public class PasswordAttribute : RegularExpressionAttribute
    {
        public PasswordAttribute()
            : base(@"((?=.*\d)(?=.*[a-z]).{6,20})")
        {
            this.ErrorMessage = "Your password must be between 6 and 20 symbols and must contains at least one letter and one digit";
        }
    }
}