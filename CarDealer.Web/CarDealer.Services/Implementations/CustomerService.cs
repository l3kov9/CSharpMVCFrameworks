namespace CarDealer.Services.Implementations
{
    using Data;
    using Data.Models;
    using Models;
    using Models.Customers;
    using Models.Sales;
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

        public void Create(string name, DateTime birthday, bool isYoungDriver)
        {
            var customer = new Customer
            {
                Name = name,
                BirthDay = birthday,
                IsYoungDriver = isYoungDriver
            };

            this.db
                .Customers
                .Add(customer);

            this.db.SaveChanges();
        }

        public void Edit(int id, string name, DateTime birthday, bool isYoungDriver)
        {
            var existingCustomer = this.db.Customers.Find(id);

            if(existingCustomer == null)
            {
                return;
            }

            existingCustomer.Name = name;
            existingCustomer.BirthDay = birthday;
            existingCustomer.IsYoungDriver = isYoungDriver;

            db.SaveChanges();
        }

        public CustomerModel ById(int id)
            => this.db
                .Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDay = c.BirthDay,
                    IsYoungDriver = c.IsYoungDriver
                })
                .FirstOrDefault();

        public CustomerTotalSalesModel TotalSalesById(int id)
            => this.db
                .Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerTotalSalesModel
                {
                    Name = c.Name,
                    BoughtCars =c.Sales.Select(s=>new SaleModel
                    {
                        Discount = s.Discount,
                        Price = s.Car.Parts.Sum(p=>p.Part.Price)
                    }),
                    IsYoungDriver = c.IsYoungDriver,
                })
                .FirstOrDefault();

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
                Id = c.Id,
                Name = c.Name,
                BirthDay = c.BirthDay,
                IsYoungDriver = c.IsYoungDriver
            })
            .ToList();
        }

        public bool Exists(int id)
            => this.db
                .Customers
                .Any(c => c.Id == id);
    }
}
