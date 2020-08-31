using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShopping.Models.Mapping
{
    public class CarMapping : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(p => p.ID);
            builder.Property(p => p.ID).UseIdentityColumn(1251,1);
            builder.Property(p => p.ID).ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.CreateDate).IsRequired();
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.Quantity).IsRequired();
            builder.Property(p => p.ColorId).IsRequired();
            builder.Property(p => p.ModelId).IsRequired();
            builder.Property(p => p.CategoryId).IsRequired();

            builder.HasOne(p => p.Model).WithMany(p => p.Cars).HasForeignKey(p => p.ModelId);
            builder.HasOne(p => p.Color).WithMany(p => p.Cars).HasForeignKey(p => p.ColorId);
            builder.HasOne(p => p.Category).WithMany(p => p.Cars).HasForeignKey(p => p.CategoryId);
            builder.HasMany(p => p.OrderDetails).WithOne(p => p.Car).HasForeignKey(p => p.CarId);
            builder.HasMany(p => p.Images).WithOne(p => p.Car).HasForeignKey(p => p.CarID);
        }
    }
}
