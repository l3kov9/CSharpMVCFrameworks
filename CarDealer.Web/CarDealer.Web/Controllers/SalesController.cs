namespace CarDealer.Web.Controllers
{
    using CarDealer.Web.Helpers.Filters;
    using Helpers.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;

    public class SalesController : Controller
    {
        private readonly ISaleService sales;
        private readonly ICustomerService customers;
        private readonly ICarService cars;

        public SalesController(ISaleService sales, ICustomerService customers, ICarService cars)
        {
            this.sales = sales;
            this.customers = customers;
            this.cars = cars;
        }

        public IActionResult All()
            => View(this.sales.All());

        public IActionResult Details(int id)
            => this.ViewOrNotFound(this.sales.Details(id));

        public IActionResult Discounted()
            => this.ViewOrNotFound(this.sales.Discounted());

        public IActionResult Add()
        {
            var model = new SaleFormModel
            {
                CarsDropdown = this.cars.GetCarsSelectListItems(),
                CustomersDropdown = this.customers.GetCustomersSelectListItems()
            };

            return View(model);
        }

        public IActionResult ReviewSale(SaleFormModel model)
        {
            var car = this.cars.ById(model.CarId);
            var customer = this.customers.ById(model.CustomerId);
            var discount = customer.IsYoungDriver ? 10.0 : 5.0;

            var review = new ReviewSaleViewModel
            {
                CarId = customer.Id,
                CustomerId = customer.Id,
                CustomerName = customer.Name,
                Car = car.Name,
                CarPrice = car.Price,
                Discount = discount,
                FinalCarPrice = car.Price * (100 - (decimal)discount)/100
            };

            return View(review);
        }

        [HttpPost]
        [Log]
        public IActionResult ReviewSale(ReviewSaleViewModel model)
        {
            this.sales.Create(model.CarId, model.CustomerId, model.Discount);

            return RedirectToAction(nameof(All));
        }
    }
}
