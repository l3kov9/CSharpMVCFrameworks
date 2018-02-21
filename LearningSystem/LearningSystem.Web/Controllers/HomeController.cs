namespace LearningSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly ICourseService courses;

        public HomeController(ICourseService courses)
        {
            this.courses = courses;
        }

        public async Task<IActionResult> Index()
            => View(await this.courses.Active());

        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
