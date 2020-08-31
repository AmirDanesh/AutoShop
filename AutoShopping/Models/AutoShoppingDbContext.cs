using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoShopping.Models.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AutoShopping.Models
{
    public class AutoShoppingDbContext : IdentityDbContext
    {

        public IPasswordHasher<IdentityUser> _passwordHasher { get; }

        public AutoShoppingDbContext(IPasswordHasher<IdentityUser> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server =.; DataBase= AutoShopping ; Trusted_Connection=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(new Category() {ID = 1, CategoryName = "سدان"});
            modelBuilder.Entity<Category>().HasData(new Category() {ID = 2, CategoryName = "کوپه"});
            modelBuilder.Entity<Category>().HasData(new Category() {ID = 3, CategoryName = "استیشن"});
            modelBuilder.Entity<Category>().HasData(new Category() {ID = 4, CategoryName = "شاسی بلند"});


            #region Mapping
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new TicketMapping());
            modelBuilder.ApplyConfiguration(new CarMapping());
            #endregion

            #region RoleTableFluents
            modelBuilder.Entity<Role>().HasKey(p => p.ID);
            modelBuilder.Entity<Role>().Property(p => p.ID)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<Role>().Property(p => p.RoleName)
                .IsRequired();
            #endregion

            #region StatusTableFluents
            modelBuilder.Entity<Status>().HasKey(p => p.ID);
            modelBuilder.Entity<Status>().Property(p => p.ID)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<Status>().Property(p => p.StatusName)
                .IsRequired();
            #endregion

            #region BrandTableFluents
            modelBuilder.Entity<Brand>().HasKey(p => p.ID);
            modelBuilder.Entity<Brand>().Property(p => p.ID)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<Brand>().Property(p => p.BrandName)
                .IsRequired()
                .HasColumnType("nVarChar(20)");

            modelBuilder.Entity<Brand>().HasMany(p => p.Models).WithOne(p => p.Brand).HasForeignKey(p => p.BrandID).OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region ModelTableFluents
            modelBuilder.Entity<Model>().HasKey(p => p.ID);
            modelBuilder.Entity<Model>().Property(p => p.ID)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<Model>().Property(p => p.ModelName)
                .IsRequired()
                .HasColumnType("nVarChar(20)");
            modelBuilder.Entity<Model>().Property(p => p.BrandID)
                .IsRequired();
            #endregion

            #region ColorTableFluents
            modelBuilder.Entity<Color>().HasKey(p => p.ID);
            modelBuilder.Entity<Color>().Property(p => p.ID)
                .IsRequired()
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Color>().Property(p => p.ColorName)
                .IsRequired()
                .HasColumnType("nVarChar(20)");
            modelBuilder.Entity<Color>().Property(p => p.ColorCode)
                .IsRequired()
                .HasColumnType("nVarChar(10)");
            #endregion

            #region CategoryTableFluents
            modelBuilder.Entity<Category>().HasKey(p => p.ID);
            modelBuilder.Entity<Category>().Property(p => p.ID)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<Category>().Property(p => p.CategoryName)
                .IsRequired();
            #endregion

            #region OrderTableFluents
            modelBuilder.Entity<Order>().HasKey(p => p.ID);
            modelBuilder.Entity<Order>().Property(p => p.ID)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<Order>().Property(p => p.OrderDate)
                .IsRequired();
            modelBuilder.Entity<Order>().Property(p => p.UserId)
                .IsRequired();

            modelBuilder.Entity<Order>().HasMany(p => p.OrderDetails).WithOne(p => p.Order).HasForeignKey(p => p.OrderId);

            #endregion

            #region OrderDetailsTableFluents
            modelBuilder.Entity<OrderDetail>().HasKey(p => p.ID);
            modelBuilder.Entity<OrderDetail>().Property(p => p.ID)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<OrderDetail>().Property(p => p.CarId)
                .IsRequired();
            modelBuilder.Entity<OrderDetail>().Property(p => p.Price)
                .IsRequired();
            modelBuilder.Entity<OrderDetail>().Property(p => p.Quantity)
                .IsRequired();
            modelBuilder.Entity<OrderDetail>().Property(p => p.OrderId)
                .IsRequired();
            #endregion

            #region TicketDetailsTableFluents
            modelBuilder.Entity<TicketDetails>().HasKey(p => p.ID);
            modelBuilder.Entity<TicketDetails>().Property(p => p.ID)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<TicketDetails>().Property(p => p.TicketId)
                .IsRequired();
            modelBuilder.Entity<TicketDetails>().Property(p => p.UserId)
                .IsRequired();
            modelBuilder.Entity<TicketDetails>().Property(p => p.Date)
                .IsRequired();
            #endregion

            #region ImageTableFluents
            modelBuilder.Entity<Images>().HasKey(p => p.ID);
            modelBuilder.Entity<Images>().Property(p => p.CarID)
                .IsRequired();
            modelBuilder.Entity<Images>().Property(p => p.Name)
                .IsRequired();
            #endregion
        }

        #region Dbset
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> orderDetails { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketDetails> TicketDetails { get; set; }
        public DbSet<Images> Images { get; set; }
        
        #endregion

    }

    
}
