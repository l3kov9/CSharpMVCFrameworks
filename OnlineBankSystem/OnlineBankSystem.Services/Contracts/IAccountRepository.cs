namespace OnlineBankSystem.Services.Contracts
{
    using Data;
    using Models.Accounts;
    using Models.Bills;
    using Models.Transactions;
    using System.Collections.Generic;

    public interface IAccountRepository : IDbConnector
    {
        AccountServiceModel GetAccountProfileByUserId(int id);

        IEnumerable<BillServiceModel> GetBillsByUserId(int id);

        bool TransferMoney(string senderIban, string receiverIban, decimal amount, string description);

        IEnumerable<TransactionServiceModel> GetLatestSixTransationsById(int id);

        IEnumerable<TransactionServiceModel> GetAllTransactionsByUserId(int userId, string direction);

        bool FinishTransactionById(int id, int currentUserId);

        bool DeleteTransaction(int id, int currentUserId);
    }
}
