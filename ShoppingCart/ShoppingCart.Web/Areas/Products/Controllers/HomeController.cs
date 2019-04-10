namespace ShoppingCart.Web.Areas.Products.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System.Threading.Tasks;

    [Area("Products")]
    public class HomeController : BaseProductController
    {
        private readonly IProductService products;

        public HomeController(IProductService products)
        {
            this.products = products;
        }

        public async Task<IActionResult> Index()
        {
            var products = await this.products.AllAsync();
            
            return View(products);
        }
    }
}
