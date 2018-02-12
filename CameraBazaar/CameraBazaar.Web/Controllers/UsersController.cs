namespace CameraBazaar.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class UsersController : Controller
    {
        private readonly IUserService users;

        public UsersController(IUserService users)
        {
            this.users = users;
        }

        public IActionResult Details(string username)
            => View(this.users.ByUsername(username));
    }
}
