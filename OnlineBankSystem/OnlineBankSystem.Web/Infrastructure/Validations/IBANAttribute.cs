namespace OnlineBankSystem.Web.Infrastructure.Validations
{
    using System.ComponentModel.DataAnnotations;

    public class IBANAttribute : RegularExpressionAttribute
    {
        public IBANAttribute()
            : base(@"((?=.*\d)(?=.*[a-z]).{22})")
        {
            this.ErrorMessage = "IBAN number must be exactly 22 symbols and must contains only letters and digits";
        }
    }
}