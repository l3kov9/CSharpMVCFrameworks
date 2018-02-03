namespace CarDealer.Services
{
    using Models;
    using Models.Customers;
    using System;
    using System.Collections.Generic;

    public interface ICustomerService
    {
        IEnumerable<CustomerModel> OrderedCustomer(OrderDirection order);

        CustomerTotalSalesModel TotalSalesById(int id);

        bool Exists(int id);

        void Create(string name, DateTime birthday, bool isYoungDriver);

        void Edit(int id, string name, DateTime birthday, bool isYoungDriver);

        CustomerModel ById(int id);
    }
}
