namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Data.Models;
    using Helpers.Extensions;
    using LearningSystem.Web.Controllers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models;
    using Services.Admin;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static Helpers.WebConstants;

    public class CoursesController : AdminBaseController
    {
        private readonly IAdminCourseService courses;
        private readonly UserManager<User> userManager;

        public CoursesController(IAdminCourseService courses, UserManager<User> userManager)
        {
            this.courses = courses;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Create()
            => View(new CreateCourseFormViewModel
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30),
                Trainers = await this.GetTrainersAsync()
            });

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Trainers = await this.GetTrainersAsync();
                return View(model);
            }

            await this.courses.CreateAsync(model.Name, model.Description, model.StartDate, model.EndDate, model.TrainerId);

            TempData.AddSuccessMessage($"Course {model.Name} successfully added.");

            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }

        private async Task<IEnumerable<SelectListItem>> GetTrainersAsync()
        {
            var trainers = await this.userManager.GetUsersInRoleAsync(TrainerRole);

            var trainerListItems = trainers
                .Select(t => new SelectListItem
                {
                    Text = t.UserName,
                    Value = t.Id
                })
                .ToList();

            return trainerListItems;
        }
    }
}
