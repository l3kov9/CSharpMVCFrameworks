namespace CarDealer.Web.Controllers
{
    using CarDealer.Web.Helpers.Filters;
    using Helpers.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Customers;
    using Services;
    using Services.Models;

    [Route("customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService customers;

        public CustomersController(ICustomerService customers)
        {
            this.customers = customers;
        }

        [Authorize]
        [Route(nameof(Create))]
        public IActionResult Create()
            => View();

        [HttpPost]
        [Authorize]
        [Log]
        [Route(nameof(Create))]
        public IActionResult Create(CustomerFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.customers.Create(model.Name,model.Birthday, model.IsYoungDriver);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var customer = this.customers.ById(id);

            if(customer == null)
            {
                return NotFound();
            }

            return View(new CustomerFormModel
            {
                Name = customer.Name,
                Birthday = customer.BirthDay,
                IsYoungDriver = customer.IsYoungDriver
            });
        }

        [HttpPost]
        [Log]
        [Authorize]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, CustomerFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var customerExists = this.customers.Exists(id);

            if (!customerExists)
            {
                return NotFound();
            }

            this.customers.Edit(id, model.Name, model.Birthday, model.IsYoungDriver);

            return RedirectToAction(nameof(All));
        }

        [Route("all/{order?}")]
        public IActionResult All(string order)
        {
            order = order ?? string.Empty;

            var orderDirection = order.ToLower() == "descending"
                                    ? OrderDirection.Descending
                                    : OrderDirection.Ascending;

            var customers = this.customers.OrderedCustomer(orderDirection);

            return View(new AllCustomerModel
            {
                Customers = customers,
                OrderDirection = orderDirection
            });
        }

        [Route("{id}")]
        public IActionResult TotalSales(int id) 
            => this.ViewOrNotFound(this.customers.TotalSalesById(id));
    }
}
