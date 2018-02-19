namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Data.Models;
    using Helpers.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Services.Admin;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController : AdminBaseController
    {
        private readonly IAdminUserService users;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(IAdminUserService users, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.users = users;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await this.users.All();
            var roles = await this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync();

            return View(new UserListingViewModel
            {
                Users = users,
                Roles = roles
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddUserToRoleFormModel model)
        {
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            var roleExists = await this.roleManager.RoleExistsAsync(model.Role);

            if(!userExists || !roleExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid identity details.");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            await this.userManager.AddToRoleAsync(user, model.Role);

            TempData.AddSuccessMessage($"User {user.UserName} successfully added to the {model.Role} role.");

            return RedirectToAction(nameof(Index));
        }
    }
}
