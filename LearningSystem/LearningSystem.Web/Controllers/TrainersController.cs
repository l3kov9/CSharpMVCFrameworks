namespace LearningSystem.Web.Controllers
{
    using Data.Models;
    using Helpers;
    using LearningSystem.Services.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Trainers;
    using Services;
    using System.Threading.Tasks;

    [Authorize(Roles = WebConstants.TrainerRole)]
    public class TrainersController : Controller
    {
        private readonly ITrainerService trainers;
        private readonly ICourseService courses;
        private readonly UserManager<User> userManager;

        public TrainersController(ITrainerService trainers, ICourseService courses, UserManager<User> userManager)
        {
            this.trainers = trainers;
            this.courses = courses;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Courses()
        {
            var userId = this.userManager.GetUserId(User);

            var courses = await this.trainers.Courses(userId);

            return View(courses);
        }

        public async Task<IActionResult> Students(int id)
        {
            var userId = this.userManager.GetUserId(User);

            if(!await this.trainers.IsTrainer(userId, id))
            {
                return NotFound();
            }

            var students = await this.trainers.StudentsInCourseAsync(id);

            return View(new StudentInCourseViewModel
            {
                Students = students,
                Course = await this.courses.ByIdAsync<CourseListingServiceModel>(id)
            });
        }

        [HttpPost]
        public async Task<IActionResult> GradeStudent(int id, string studentId, Grade grade)
        {
            var userId = this.userManager.GetUserId(User);

            if (string.IsNullOrEmpty(studentId) || !await this.trainers.IsTrainer(userId, id))
            {
                return BadRequest();
            }

            var success = await this.trainers.GradeStudentAsync(id, studentId, grade);

            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Students), new { id });
        }
    }
}
