namespace OnlineBankSystem.Web.Models.Accounts
{
    using Infrastructure.Validations;
    using System.ComponentModel.DataAnnotations;

    public class TransactionViewModel
    {
        [Required(ErrorMessage = "Please choose your IBAN number")]
        [IBAN]
        [Display(Name = "My IBAN with amount")]
        public string SenderIban { get; set; }

        [Required(ErrorMessage = "Please choose IBAN number receiver")]
        [IBAN]
        public string ReceiverIban { get; set; }
        
        [Required(ErrorMessage = "You can't transfer less than 10 $")]
        [Range(10.0, double.MaxValue, ErrorMessage = "You can't transfer less than 10 $")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Please add your reason for the transfer")]
        [MinLength(6, ErrorMessage = "Description must be between 6 and 32 symbols")]
        [MaxLength(32, ErrorMessage = "Description must be between 6 and 32 symbols")]
        public string Description { get; set; }
    }
}