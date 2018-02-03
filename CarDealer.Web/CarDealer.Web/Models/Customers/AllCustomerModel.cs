namespace CarDealer.Web.Models.Customers
{
    using System.Collections.Generic;
    using Services.Models;
    using Services.Models.Customers;

    public class AllCustomerModel
    {
        public IEnumerable<CustomerModel> Customers { get; set; }

        public OrderDirection OrderDirection { get; set; }
    }
}
