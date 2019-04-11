namespace OnlineBankSystem.Services.Models.Transactions
{
    public class TransactionServiceModel
    {
        public TransactionServiceModel(int id, string senderIban, string receiverIban, decimal amount, string description, bool isFinished)
        {
            this.Id = id;
            this.SenderIban = senderIban;
            this.ReceiverIban = receiverIban;
            this.Amount = amount;
            this.Description = description;
            this.IsFinished = isFinished;
        }

        public int Id { get; set; }

        public string SenderIban { get; set; }

        public string ReceiverIban { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public bool IsFinished { get; set; }
    }
}
