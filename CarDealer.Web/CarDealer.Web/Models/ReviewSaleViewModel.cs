namespace CarDealer.Web.Models
{
    public class ReviewSaleViewModel
    {
        public int CarId { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string Car { get; set; }

        public double Discount { get; set; }

        public decimal CarPrice { get; set; }

        public decimal FinalCarPrice { get; set; }
    }
}
