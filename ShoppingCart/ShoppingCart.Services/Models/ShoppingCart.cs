namespace ShoppingCart.Services.Models
{
    using System.Collections.Generic;

    public class ShoppingCart
    {
        public IList<ItemCart> Products { get; set; }
    }
}
