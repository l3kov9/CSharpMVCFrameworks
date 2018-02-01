namespace CarDealer.Services.Implementations
{
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CustomerModel> OrderedCustomer(OrderDirection order)
        {
            var customersQuery = this.db.Customers.AsQueryable();

            switch (order)
            {
                case OrderDirection.Ascending:
                    customersQuery = customersQuery
                        .OrderBy(c => c.BirthDay)
                        .ThenBy(c => c.IsYoungDriver);
                    break;

                case OrderDirection.Descending:
                    customersQuery = customersQuery
                        .OrderByDescending(c => c.BirthDay)
                        .ThenBy(c => c.IsYoungDriver);
                    break;

                default: throw new InvalidOperationException($"Invalid order: {order}");
            }

            return customersQuery.Select(c => new CustomerModel
            {
                Name = c.Name,
                BirthDay = c.BirthDay,
                IsYoungDriver = c.IsYoungDriver
            })
            .ToList();
        }
    }
}
