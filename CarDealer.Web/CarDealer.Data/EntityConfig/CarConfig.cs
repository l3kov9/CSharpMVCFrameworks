namespace CarDealer.Data.EntityConfig
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CarConfig : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder
                .HasMany(c => c.Sales)
                .WithOne(s => s.Car)
                .HasForeignKey(s => s.CarId);

            builder
                .HasMany(c => c.Parts)
                .WithOne(pc => pc.Car)
                .HasForeignKey(pc => pc.CarId);
        }
    }
}
