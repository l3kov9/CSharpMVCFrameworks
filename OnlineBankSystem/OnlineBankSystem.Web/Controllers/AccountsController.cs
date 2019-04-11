namespace OnlineBankSystem.Web.Controllers
{
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Models.Accounts;
    using Services;
    using Services.Extensions;
    using System.Web.Mvc;

    [AuthorizeUser]
    public class AccountsController : Controller
    {
        private AccountService accounts;

        public AccountsController()
            : this(new AccountService())
        {
        }

        public AccountsController(AccountService accounts)
        {
            this.accounts = accounts;
        }

        public ActionResult Index()
        {
            var userId = this.User.GetUserId();
            var profile = this.accounts.GetProfileBillsById(userId);

            return View(profile);
        }

        public ActionResult AllTransactions(string direction = "descending")
        {
            var userId = this.User.GetUserId();
            var transactions = this.accounts.GetAllTransactionsByUserId(userId, direction);

            return View(transactions);
        }

        [HttpGet]
        public ActionResult Transactions()
        {
            var userId = this.User.GetUserId();
            var profileBills = this.accounts.GetBillsById(userId);
            this.ViewBag.Bills = profileBills;

            return View();
        }

        [HttpPost]
        public ActionResult Transactions(TransactionViewModel transaction)
        {
            if (!ModelState.IsValid)
            {
                var userId = this.User.GetUserId();
                var profileBills = this.accounts.GetBillsById(userId);
                this.ViewBag.Bills = profileBills;

                return View(transaction);
            }

            var successfullTransaction = this.accounts.TransferMoney(transaction.SenderIban, transaction.ReceiverIban, transaction.Amount, transaction.Description);

            if (!successfullTransaction)
            {
                this.TempData.AddErrorMessage("Invalid transaction");

                return this.View(transaction);
            }
            else
            {
                this.TempData.AddSuccessMessage("Your money transfer was successful");

                return this.RedirectToAction("Index", "Home");
            }
        }

        public ActionResult FinishTransaction(int id)
        {
            var currentUserId = this.User.GetUserId();
            var isTransactionFinished = this.accounts.FinishTransactionById(id, currentUserId);

            if (!isTransactionFinished)
            {
                this.TempData.AddErrorMessage("You've failed to finish the transaction");
            }
            else
            {
                this.TempData.AddSuccessMessage("Your transaction was finished successfully");
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult CancelTransaction(int id)
        {
            var currentUserId = this.User.GetUserId();
            var isTransactionDeleted = this.accounts.DeleteTransaction(id, currentUserId);

            if (!isTransactionDeleted)
            {
                this.TempData.AddErrorMessage("You've failed to delete the transaction");
            }
            else
            {
                this.TempData.AddSuccessMessage("Your transaction was deleted successfully");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}