namespace OnlineBankSystem.Services.Models.Bills
{
    public class BillServiceModel
    {
        public BillServiceModel(string iban, decimal amount)
        {
            this.IBAN = iban;
            this.Amount = amount;
        }

        public string IBAN { get; set; }

        public decimal Amount { get; set; }
    }
}
