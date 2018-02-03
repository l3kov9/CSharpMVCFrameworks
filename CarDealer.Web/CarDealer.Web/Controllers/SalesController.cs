namespace CarDealer.Web.Controllers
{
    using Helpers.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    [Route("sales")]
    public class SalesController : Controller
    {
        private readonly ISaleService sales;

        public SalesController(ISaleService sales)
        {
            this.sales = sales;
        }

        [Route("")]
        public IActionResult All()
            => View(this.sales.All());

        [Route("{id}", Order = 2)]
        public IActionResult Details(int id)
            => this.ViewOrNotFound(this.sales.Details(id));

        [Route("discounted", Order = 1)]
        public IActionResult Discounted()
            => this.ViewOrNotFound(this.sales.Discounted());
    }
}
