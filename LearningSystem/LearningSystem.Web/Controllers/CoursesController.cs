namespace LearningSystem.Web.Controllers
{
    using Data.Models;
    using Helpers.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Courses;
    using Services;
    using Services.Models;
    using System.Threading.Tasks;

    public class CoursesController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ICourseService courses;

        public CoursesController(UserManager<User> userManager, ICourseService courses)
        {
            this.userManager = userManager;
            this.courses = courses;
        }

        public async Task<IActionResult> Details(int id)
        {
            var course = new CourseDetailsViewModel
            {
                Course = await this.courses.ByIdAsync<CourseDetailsServiceModel>(id)
            };

            if (course.Course == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                course.UserIsEnrolled = await this.courses.UserIsEnrolledInCourseByIdAsync(this.userManager.GetUserId(User), id);
            }

            return View(course);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SignUp(int id)
        {
            var userId = this.userManager.GetUserId(User);

            bool success = await this.courses.AddStudentToCourseAsync(userId, id);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("You successfully joined this course!");

            return RedirectToAction(nameof(Details), new { id });
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SignOut(int id)
        {
            var userId = this.userManager.GetUserId(User);

            bool success = await this.courses.RemoveStudentFromCourseAsync(userId, id);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Sorry to see you out!");

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
