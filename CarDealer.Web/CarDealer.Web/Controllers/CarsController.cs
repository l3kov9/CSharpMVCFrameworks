namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Cars;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CarsController : Controller
    {
        private const int PageSize = 10;

        private readonly ICarService cars;
        private readonly IPartService parts;

        public CarsController(ICarService cars, IPartService parts)
        {
            this.cars = cars;
            this.parts = parts;
        }

        [Authorize]
        public IActionResult Create()
            => View(new CarFormModel
            {
                AllParts = this.GetPartsSelectItems()
            });

        [Authorize]
        [HttpPost]
        public IActionResult Create(CarFormModel carModel)
        {
            if (!ModelState.IsValid)
            {
                carModel.AllParts = this.GetPartsSelectItems();
                return View(carModel);
            }

            this.cars.Create(carModel.Make, carModel.Model, carModel.TravelledDistance, carModel.SelectedParts);

            return RedirectToAction(nameof(Parts));
        }

        public IActionResult Delete(int id)
            => View(id);

        public IActionResult Destroy(int id)
        {
            this.cars.Delete(id);

            return RedirectToAction(nameof(Parts));
        }

        public IActionResult Makes(string make)
        {
            var carsByMake = this.cars.ByMake(make);

            return View(new CarsByMakeModel
            {
                Make = make,
                Cars = carsByMake
            });
        }

        public IActionResult Parts(int page = 1)
            => View(new CarPageListingModel
            {
                Cars = this.cars.WithParts(page, PageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.cars.Total() / (double)PageSize)
            });

        private IEnumerable<SelectListItem> GetPartsSelectItems()
            => this.parts
                     .All()
                     .Select(p => new SelectListItem
                     {
                         Text = p.Name,
                         Value = p.Id.ToString()
                     });
    }
}
