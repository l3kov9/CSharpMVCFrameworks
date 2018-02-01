namespace CarDealer.Web.Models.Customers
{
    using System.Collections.Generic;
    using Services.Models;

    public class AllCustomerModel
    {
        public IEnumerable<CustomerModel> Customers { get; set; }

        public OrderDirection OrderDirection { get; set; }
    }
}
