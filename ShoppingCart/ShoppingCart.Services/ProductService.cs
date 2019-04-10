namespace ShoppingCart.Services
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly ShoppingCartDbContext db;

        public ProductService(ShoppingCartDbContext db)
        {
            this.db = db;
        }

        public async Task AddToCart(string id, ItemCart cart)
        {

        }

        public async Task<IEnumerable<ProductServiceModel>> AllAsync()
            => await this.db
                .Products
                .Select(p => new ProductServiceModel
                {
                    Name = p.Name,
                    Price = p.Price
                })
                .ToListAsync();

        public async Task RemoveCart(string Id, int productId)
        {

        }

        public async Task SeedData()
        {
            for (int i = 0; i < 20; i++)
            {
                this.db.Add(new Product
                {
                    Name = $"Product {i + 1}",
                    Price = (i + 1) * 100
                });
            }

            await this.db.SaveChangesAsync();
        }
    }
}
