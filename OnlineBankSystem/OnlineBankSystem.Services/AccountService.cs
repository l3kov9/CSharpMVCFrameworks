namespace OnlineBankSystem.Services
{
    using Contracts;
    using Models.Accounts;
    using Models.Bills;
    using Models.Transactions;
    using SqlService;
    using System.Collections.Generic;

    public class AccountService
    {
        private IAccountRepository accountRepository;

        public AccountService()
            : this(new AccountRepository())
        {
        }

        public AccountService(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public IEnumerable<TransactionServiceModel> GetLatestSixTransationsById(int id)
        {
            return this.accountRepository.GetLatestSixTransationsById(id);
        }

        public IEnumerable<TransactionServiceModel> GetAllTransactionsByUserId(int userId, string direction)
        {
            return this.accountRepository.GetAllTransactionsByUserId(userId, direction);
        }

        public AccountServiceModel GetProfileBillsById(int id)
        {
            return this.accountRepository.GetAccountProfileByUserId(id);
        }

        public IEnumerable<BillServiceModel> GetBillsById(int userId)
        {
            return this.accountRepository.GetBillsByUserId(userId);
        }

        public bool TransferMoney(string senderIban, string receiverIban, decimal amount, string description)
        {
            return this.accountRepository.TransferMoney(senderIban, receiverIban, amount, description);
        }

        public bool FinishTransactionById(int id, int currentUserId)
        {
            return this.accountRepository.FinishTransactionById(id, currentUserId);
        }

        public bool DeleteTransaction(int id, int currentUserId)
        {
            return this.accountRepository.DeleteTransaction(id, currentUserId);
        }
    }
}
