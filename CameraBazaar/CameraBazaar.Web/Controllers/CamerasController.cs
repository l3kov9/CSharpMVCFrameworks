namespace CameraBazaar.Web.Controllers
{
    using CameraBazaar.Services.Models;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult Edit(int id)
        {
            var camera = this.cameras.ById(id);

            return View(new CameraViewModel
            {
                Id = id,
                Make = camera.Make,
                Model = camera.Model,
                Description = camera.Description,
                ImageUrl = camera.ImageUrl,
                IsFullFrame = camera.IsFullFrame,
                LightMetering = camera.LightMetering,
                MaxISO = camera.MaxISO,
                MinISO = camera.MinISO,
                MaxShutterSpeed = camera.MaxShutterSpeed,
                MinShutterSpeed = camera.MinShutterSpeed,
                Price = camera.Price,
                Quantity = camera.Quantity,
                VideoResolution = camera.VideoResolution,
                UserId = camera.UserId,
                Username =camera.Username
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(CameraViewModel cameraModel)
        {
            var user = await this.userManager.GetUserAsync(User);

            this.cameras.Edit(cameraModel.Id,
                cameraModel.Make,
                cameraModel.Model,
                cameraModel.Description,
                cameraModel.ImageUrl,
                cameraModel.IsFullFrame,
                cameraModel.LightMetering,
                cameraModel.MaxISO,
                cameraModel.MinISO,
                cameraModel.MinShutterSpeed,
                cameraModel.MaxShutterSpeed,
                cameraModel.Price,
                cameraModel.Quantity,
                user.Id,
                cameraModel.VideoResolution);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var camera = this.cameras.ById(id);

            return View(new CameraDeleteViewModel
            {
                Id = id,
                Make = camera.Make.ToString(),
                Model = camera.Model
            });
        }

        [Authorize]
        public IActionResult Destroy(CameraDeleteViewModel cameraModel)
        {
            this.cameras.DeleteById(cameraModel.Id);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All()
            => View(this.cameras.All());

        [Authorize]
        public IActionResult Add()
            => View();

        [Authorize]
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

        [Authorize]
        public IActionResult Success()
        {
            return View();
        }
    }
}
