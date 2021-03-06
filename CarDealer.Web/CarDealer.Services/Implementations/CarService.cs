﻿namespace CarDealer.Services.Implementations
{
    using Data;
    using Data.Models;
    using Models.Parts;
    using Models.Cars;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CarService : ICarService
    {
        private readonly CarDealerDbContext db;

        public CarService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public CarSaleServiceModel ById(int carId)
            => this.db
                .Cars
                .Where(c => c.Id == carId)
                .Select(c => new CarSaleServiceModel
                {
                    Name = $"{c.Make} {c.Model}",
                    Price = c.Parts.Sum(p => p.Part.Price)
                })
                .FirstOrDefault();

        public IEnumerable<CarModel> ByMake(string make)
             => this.db
                .Cars
                .Where(c => c.Make.ToLower() == make.ToLower())
                .OrderBy(c => c.Model)
                .ThenBy(c => c.TravelledDistance)
                .Select(c => new CarModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToList();

        public void Create(string make, string model, long travelledDistance, IEnumerable<int> parts)
        {
            var existingPartIds = this.db
                    .Parts
                    .Where(p => parts.Contains(p.Id))
                    .Select(p => p.Id)
                    .ToList();
            
            var car = new Car
            {
                Make = make,
                Model = model,
                TravelledDistance = travelledDistance
            };
            
            foreach (var partId in existingPartIds)
            {
                car.Parts.Add(new PartCar { PartId = partId });
            }

            this.db.Add(car);
            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            var car = this.db.Cars.Find(id);

            if(car == null)
            {
                return;
            }

            this.db.Cars.Remove(car);
            this.db.SaveChanges();
        }

        public IEnumerable<SelectListItem> GetCarsSelectListItems()
            => this.db
                .Cars
                .Select(c => new SelectListItem
                {
                    Text = $"{c.Make} {c.Model}",
                    Value = c.Id.ToString()
                })
                .ToList();

        public int Total()
            => this.db
                .Cars
                .Count();

        public IEnumerable<CarWithPartsModel> WithParts(int page = 1, int pageSize = 10)
            => this.db
                .Cars
                .OrderByDescending(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CarWithPartsModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.Parts.Select(p => new PartModel
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price
                    })
                })
                .ToList();
    }
}
