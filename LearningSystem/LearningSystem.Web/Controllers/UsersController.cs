namespace LearningSystem.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System.Threading.Tasks;

    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService users;

        public UsersController(UserManager<User> userManager, IUserService users)
        {
            this.userManager = userManager;
            this.users = users;
        }

        public async Task<IActionResult> Profile(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            if(user == null)
            {
                return NotFound();
            }
            
            var profile = await this.users.ProfileAsync(user.Id);

            return View(profile);
        }
    }
}
