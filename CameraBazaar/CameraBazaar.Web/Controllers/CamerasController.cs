namespace CameraBazaar.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Cameras;
    using Services;
    using System.Threading.Tasks;

    public class CamerasController : Controller
    {
        private ICameraService cameras;
        private readonly UserManager<User> userManager;

        public CamerasController(ICameraService cameras, UserManager<User> userManager)
        {
            this.cameras = cameras;
            this.userManager = userManager;
        }

        public IActionResult Details(int id)
        {
            var camera = this.cameras.ById(id);

            return View(camera);
        }

        public IActionResult All()
            => View(this.cameras.All());

        public IActionResult Add()
            => View();

        [HttpPost]
        public async Task<IActionResult> Add(AddCameraViewModel cameraModel)
        {
            if (!ModelState.IsValid)
            {
                return View(cameraModel);
            }

            var user = await userManager.GetUserAsync(HttpContext.User);

            this.cameras.Create(cameraModel.Make,
                cameraModel.Model,
                cameraModel.Price,
                cameraModel.Quantity,
                cameraModel.MinShutterSpeed,
                cameraModel.MaxShutterSpeed,
                cameraModel.MinISO,
                cameraModel.MaxISO,
                cameraModel.IsFullFrame,
                cameraModel.VideoResolution,
                cameraModel.LightMetering,
                cameraModel.Description,
                cameraModel.ImageUrl,
               user.Id);

            TempData["model"]= $"{cameraModel.Model} - {cameraModel.Make}";

            return RedirectToAction(nameof(Success));
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
