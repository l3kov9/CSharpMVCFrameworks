namespace OnlineBankSystem.Services.SqlService
{
    using Common.SqlServer;
    using Contracts;
    using Data;
    using Models.Accounts;
    using Models.Bills;
    using OnlineBankSystem.Services.Models.Transactions;
    using System.Collections.Generic;

    public class AccountRepository : DbConnector, IAccountRepository
    {
        public AccountRepository()
        {
        }

        public AccountRepository(string connectionString) : base(connectionString)
        {
        }

        public bool DeleteTransaction(int id, int currentUserId)
        {
            var reader = this.ExecuteReader(QueryConstants.CheckIfTransferIsCorrectWithIdAndUserId, new Dictionary<string, object>
            {
                { "@userId", currentUserId },
                { "@transactionId", id }
            });

            if (!reader.HasRows)
            {
                return false;
            }

            reader.Close();

            var result = this.ExecuteNonQuery(QueryConstants.DeleteTransfer, new Dictionary<string, object> { { "@transactionId", id } });

            return result == 1;
        }

        public bool FinishTransactionById(int id, int currentUserId)
        {
            var reader = this.ExecuteReader(QueryConstants.CheckIfTransferIsCorrectWithIdAndUserId, new Dictionary<string, object>
            {
                { "@userId", currentUserId },
                { "@transactionId", id }
            });

            if (!reader.HasRows)
            {
                return false;
            }

            reader.Close();

            var transactionReader = this.ExecuteReader(QueryConstants.GetTransactionById, new Dictionary<string, object> { { "@transactionId", id } });

            if (!transactionReader.HasRows)
            {
                return false;
            }

            TransactionServiceModel transaction = null;

            while (transactionReader.Read())
            {
                var transactionId = (int)transactionReader[0];
                var senderIban = (string)transactionReader[1];
                var receiverIban = (string)transactionReader[2];
                var amount = (decimal)transactionReader[3];
                var description = (string)transactionReader[4];
                var isFinished = (bool)transactionReader[5];

                transaction = new TransactionServiceModel(transactionId, senderIban, receiverIban, amount, description, isFinished);
            }

            transactionReader.Close();

            var senderIbanReader = this.ExecuteReader(QueryConstants.GetBillByIBAN, new Dictionary<string, object> { { "@iban", transaction.SenderIban } });

            if (!senderIbanReader.HasRows)
            {
                return false;
            }

            BillServiceModel senderBill = null;

            while (senderIbanReader.Read())
            {
                var iban = (string)senderIbanReader[0];
                var senderAmount = (decimal)senderIbanReader[1];

                senderBill = new BillServiceModel(iban, senderAmount);
            }

            senderIbanReader.Close();

            var receiverIbanReader = this.ExecuteReader(QueryConstants.GetBillByIBAN, new Dictionary<string, object> { { "@iban", transaction.ReceiverIban } });

            if (!receiverIbanReader.HasRows)
            {
                return false;
            }

            BillServiceModel receiverBill = null;

            while (receiverIbanReader.Read())
            {
                var iban = (string)receiverIbanReader[0];
                var senderAmount = (decimal)receiverIbanReader[1];

                receiverBill = new BillServiceModel(iban, senderAmount);
            }

            receiverIbanReader.Close();

            if (senderBill.Amount < transaction.Amount)
            {
                return false;
            }

            var senderLeftMoney = senderBill.Amount -= transaction.Amount;
            var receiverLeftMoney = receiverBill.Amount += transaction.Amount;

            var updateSenderAmount = this.ExecuteNonQuery(QueryConstants.UpdateBill, new Dictionary<string, object>
            {
                { "@amount",  senderLeftMoney},
                { "@iban", senderBill.IBAN }
            });

            if (updateSenderAmount != 1)
            {
                return false;
            }

            var updateReceiverAmount = this.ExecuteNonQuery(QueryConstants.UpdateBill, new Dictionary<string, object>
            {
                { "@amount",  receiverLeftMoney},
                { "@iban", receiverBill.IBAN }
            });

            if (updateReceiverAmount != 1)
            {
                return false;
            }

            var updateTransferToFinish = this.ExecuteNonQuery(QueryConstants.UpdateTransferToFinished, new Dictionary<string, object> { { "@transactionId", id } });

            return updateTransferToFinish == 1;
        }

        public AccountServiceModel GetAccountProfileByUserId(int id)
        {
            var reader = this.ExecuteReader(QueryConstants.GetAccountBillsByUserId, new Dictionary<string, object> { { "@userId", id } });
            var counter = 0;

            AccountServiceModel account = null;

            while (reader.Read())
            {
                counter++;

                if (counter == 1)
                {
                    var username = (string)reader[0];
                    var firstName = (string)reader[1];
                    var lastName = (string)reader[2];

                    account = new AccountServiceModel(username, firstName, lastName);
                }

                var IBAN = (string)reader[3];
                var amount = (decimal)reader[4];

                var bill = new BillServiceModel(IBAN, amount);

                account.Bills.Add(bill);
            }

            return account;
        }

        public IEnumerable<BillServiceModel> GetBillsByUserId(int id)
        {
            var reader = this.ExecuteReader(QueryConstants.GetBillsByUserId, new Dictionary<string, object> { { "userId", id } });

            var bills = new List<BillServiceModel>();

            while (reader.Read())
            {
                var iban = (string)reader[0];
                var amount = (decimal)reader[1];

                var bill = new BillServiceModel(iban, amount);

                bills.Add(bill);
            }

            return bills;
        }

        public IEnumerable<TransactionServiceModel> GetLatestSixTransationsById(int id)
        {
            var reader = this.ExecuteReader(QueryConstants.GetAllTransfersByUserId, new Dictionary<string, object> { { "userId", id } });

            var transactions = new List<TransactionServiceModel>();

            while (reader.Read())
            {
                var transferId = (int)reader[0];
                var senderIban = (string)reader[1];
                var receiverIban = (string)reader[2];
                var amount = (decimal)reader[3];
                var description = (string)reader[4];
                var isFinished = (bool)reader[5];

                var transaction = new TransactionServiceModel(transferId, senderIban, receiverIban, amount, description, isFinished);

                transactions.Add(transaction);
            }

            return transactions;
        }

        public IEnumerable<TransactionServiceModel> GetAllTransactionsByUserId(int userId, string direction)
        {
            string query = string.Empty;

            if(direction == "notfinished")
            {
                query = QueryConstants.GetAllTransactionByUserIdOrderedByNotFinished;
            }
            else
            {
                query = QueryConstants.GetAllTransactionsByUserId;
            }

            var reader = this.ExecuteReader(query, new Dictionary<string, object> { { "userId", userId } });

            var transactions = new List<TransactionServiceModel>();

            while (reader.Read())
            {
                var transferId = (int)reader[0];
                var senderIban = (string)reader[1];
                var receiverIban = (string)reader[2];
                var amount = (decimal)reader[3];
                var description = (string)reader[4];
                var isFinished = (bool)reader[5];

                var transaction = new TransactionServiceModel(transferId, senderIban, receiverIban, amount, description, isFinished);

                transactions.Add(transaction);
            }

            return transactions;
        }

        public bool TransferMoney(string senderIban, string receiverIban, decimal amount, string description)
        {
            var senderIbanReader = this.ExecuteReader(QueryConstants.GetBillByIBAN, new Dictionary<string, object> { { "@iban", senderIban } });

            if (!senderIbanReader.HasRows)
            {
                return false;
            }

            BillServiceModel senderBill = null;

            while (senderIbanReader.Read())
            {
                var iban = (string)senderIbanReader[0];
                var senderAmount = (decimal)senderIbanReader[1];

                senderBill = new BillServiceModel(iban, senderAmount);
            }

            senderIbanReader.Close();

            var receiverIbanReader = this.ExecuteReader(QueryConstants.GetBillByIBAN, new Dictionary<string, object> { { "@iban", receiverIban } });

            if (!receiverIbanReader.HasRows)
            {
                return false;
            }

            BillServiceModel receiverBill = null;

            while (receiverIbanReader.Read())
            {
                var iban = (string)receiverIbanReader[0];
                var senderAmount = (decimal)receiverIbanReader[1];

                receiverBill = new BillServiceModel(iban, senderAmount);
            }

            receiverIbanReader.Close();

            if (senderBill.Amount < amount)
            {
                return false;
            }

            var result = this.ExecuteNonQuery(QueryConstants.InsertIntoTransfers, new Dictionary<string, object>
            {
                { "@senderIban", senderIban },
                { "@receiverIban", receiverIban },
                { "@amount", amount },
                { "@description", description },
                { "@isFinished", false }
            });

            return result == 1;
        }
    }
}
