namespace ShoppingCart.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task<IEnumerable<ProductServiceModel>> AllAsync();

        Task AddToCart(string id, ItemCart cart);

        Task RemoveCart(string Id, int productId);

        Task SeedData();
    }
}
