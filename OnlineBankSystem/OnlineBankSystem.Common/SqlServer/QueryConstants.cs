namespace OnlineBankSystem.Common.SqlServer
{
    public class QueryConstants
    {
        public const string GetUserByUsername = @"SELECT Id, Username, FirstName, LastName, PasswordHash, PasswordSalt FROM Users WHERE Username = @username";

        public const string GetAccountBillsByUserId = @"SELECT u.Username, u.FirstName, u.LastName, b.IBAN, b.Amount FROM Users AS u
JOIN UserBills AS ub ON ub.UserId = u.Id
JOIN Bills AS b ON b.Id = ub.BillId
WHERE u.Id = @userId";

        public const string GetBillsByUserId = @"SELECT b.IBAN, b.Amount FROM Bills AS b
JOIN UserBills AS ub ON ub.BillId = b.Id
WHERE ub.UserId = @userId";

        public const string GetBillByIBAN = @"SELECT IBAN, Amount FROM Bills
WHERE IBAN = @iban";

        public const string InsertIntoTransfers = @"INSERT INTO Transfers (SenderIBAN, ReceiverIBAN, Amount, Description, IsFinished)
VALUES ( @senderIban , @receiverIban , @amount , @description, @isFinished )";

        public const string GetAllTransfersByUserId = @"SELECT Top 6 tr.Id, tr.SenderIBAN, tr.ReceiverIBAN, tr.Amount, tr.Description, tr.IsFinished FROM Transfers AS tr
JOIN Bills AS b ON b.IBAN = tr.SenderIBAN OR b.IBAN = tr.ReceiverIBAN
JOIN UserBills AS ub ON ub.BillId = b.Id
JOIN Users As u ON u.Id = ub.UserId
WHERE u.Id = @userId
ORDER BY tr.Id Desc";

        public const string GetAllTransactionsByUserId = @"SELECT tr.Id, tr.SenderIBAN, tr.ReceiverIBAN, tr.Amount, tr.Description, tr.IsFinished FROM Transfers AS tr
JOIN Bills AS b ON b.IBAN = tr.SenderIBAN OR b.IBAN = tr.ReceiverIBAN
JOIN UserBills AS ub ON ub.BillId = b.Id
JOIN Users As u ON u.Id = ub.UserId
WHERE u.Id = @userId
ORDER BY tr.Id Desc";

        public const string GetAllTransactionByUserIdOrderedByNotFinished = @"SELECT tr.Id, tr.SenderIBAN, tr.ReceiverIBAN, tr.Amount, tr.Description, tr.IsFinished FROM Transfers AS tr
JOIN Bills AS b ON b.IBAN = tr.SenderIBAN OR b.IBAN = tr.ReceiverIBAN
JOIN UserBills AS ub ON ub.BillId = b.Id
JOIN Users As u ON u.Id = ub.UserId
WHERE u.Id = @userId
ORDER BY tr.IsFinished";

        public const string CheckIfTransferIsCorrectWithIdAndUserId = @"SELECT * FROM Transfers AS tr
JOIN Bills AS b ON b.IBAN = tr.SenderIBAN OR b.IBAN = tr.ReceiverIBAN
JOIN UserBills AS ub ON ub.BillId = b.Id
JOIN Users As u ON u.Id = ub.UserId
WHERE u.Id = @userId
AND tr.Id = @transactionId";

        public const string GetTransactionById = @"SELECT Id, SenderIBAN, ReceiverIBAN, Amount, Description, IsFinished FROM Transfers
WHERE Id = @transactionId";

        public const string UpdateTransferToFinished = @"UPDATE Transfers
SET IsFinished = 1
WHERE Id = @transactionId;";

        public const string DeleteTransfer = @"DELETE FROM Transfers
WHERE Id = @transactionId";

        public const string UpdateBill = @"UPDATE Bills
SET Amount = @amount
WHERE IBAN = @iban";
    }
}
