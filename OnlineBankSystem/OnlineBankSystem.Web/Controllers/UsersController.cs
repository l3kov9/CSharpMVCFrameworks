namespace OnlineBankSystem.Web.Controllers
{
    using Common.Authentication;
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Models.Users;
    using Services;
    using System.Web.Mvc;

    public class UsersController : Controller
    {
        private UserService users;

        public UsersController()
            : this(new UserService())
        {
        }

        public UsersController(UserService users)
        {
            this.users = users;
        }

        [HttpGet]
        [RedirectLoggedUser]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [RedirectLoggedUser]
        public ActionResult Login(LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var loggedUser = this.users.GetUser(user.Username, user.Password);

            if (loggedUser == null)
            {
                this.TempData.AddErrorMessage("Invalid Login!");

                return this.View(user);
            }
            else
            {
                this.Session[AuthenticationConstants.SessionUserKey] = loggedUser;
                this.TempData.AddSuccessMessage(string.Format("Welcome {0}", loggedUser.Username));

                return this.RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            this.Session[AuthenticationConstants.SessionUserKey] = null;
            this.TempData.AddErrorMessage("You've successfully logged out");

            return RedirectToAction("Index", "Home");
        }
    }
}