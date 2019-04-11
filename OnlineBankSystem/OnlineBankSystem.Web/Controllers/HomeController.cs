namespace OnlineBankSystem.Web.Controllers
{
    using Infrastructure.Filters;
    using Services;
    using Services.Extensions;
    using System.Web.Mvc;

    [AuthorizeUser]
    public class HomeController : Controller
    {
        private AccountService accounts;

        public HomeController()
            : this(new AccountService())
        {
        }

        public HomeController(AccountService accounts)
        {
            this.accounts = accounts;
        }

        public ActionResult Index()
        {
            var userId = this.User.GetUserId();
            var latestSixTransactions = this.accounts.GetLatestSixTransationsById(userId);

            return View(latestSixTransactions);
        }
    }
}