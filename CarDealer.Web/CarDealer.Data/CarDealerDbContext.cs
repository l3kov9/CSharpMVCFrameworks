namespace CarDealer.Data
{
    using EntityConfig;
    using Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class CarDealerDbContext : IdentityDbContext<User>
    {
        public CarDealerDbContext(DbContextOptions<CarDealerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<PartCar> PartCars { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new CarConfig());

            builder
                .ApplyConfiguration(new SaleConfig());

            builder
                .ApplyConfiguration(new PartCarConfig());

            builder
                .ApplyConfiguration(new PartConfig());

            base.OnModelCreating(builder);
        }
    }
}
