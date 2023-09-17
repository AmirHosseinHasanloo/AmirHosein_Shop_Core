using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class EshopContext : DbContext
    {
        private readonly string connectionstring = "Data Source=.;Initial Catalog=AmirShop_DB;User Id=sa;Password=asadasad";
        public EshopContext(DbContextOptions<EshopContext> options) : base(options)
        {
            //From the context, I understood from which database and in what direction I made it and am using it
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<EshopContext>();
            dbContextOptionsBuilder.UseSqlServer(connectionstring);
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryToProduct> CategoryToProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShopItem> shopItems { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryToProduct>().HasKey(t => new { t.ProductId, t.CategoryId });

            //modelBuilder.Entity<Product>
            //modelBuilder.Entity<Product>
            //    (
            //    p =>
            //    {
            //        I specified the primary key
            //        p.HasKey(w => w.Id);
            //        I specified the relation ship
            //        p.OwnsOne<ShopItem>(w => w.Item);
            //        I specified the relation ship => is one by one & I specified the(ForeignKey)
            //        p.HasOne<ShopItem>(w => w.Item).WithOne(w => w.Product).HasForeignKey<ShopItem>(w => w.Id);
            //    }
            //    );

            modelBuilder.Entity<ShopItem>
                (
                sp =>
                {
                    sp.Property(w => w.Price).HasColumnType("Money");
                    sp.HasKey(w => w.Id);
                }
                );


            #region Seed Data Category
            modelBuilder.Entity<Category>().HasData
            (
            new Category
            {
                Id = 2,
                CategoryName = "Asp.Net Core",
                Description = "Asp.Net Core 3"
            },
            new Category
            {
                Id = 3,
                CategoryName = "Asp.Net MVC",
                Description = "Asp.Net MVC 5"
            },
            new Category
            {
                Id = 4,
                CategoryName = "لباس ورزشی",
                Description = "گروه لباس ورزشی"
            },
            new Category
            {
                Id = 5,
                CategoryName = "ساعت مچی",
                Description = "گروه ساعت مچی"
            }

            );

            modelBuilder.Entity<ShopItem>().HasData
                (
                new ShopItem
                {
                    Id = 1,
                    Price = 15000.0M,
                    QuantityInStock = 4,
                },
                 new ShopItem
                 {
                     Id = 2,
                     Price = 25000.0M,
                     QuantityInStock = 21,
                 },
                  new ShopItem
                  {
                      Id = 3,
                      Price = 42000.0M,
                      QuantityInStock = 47,
                  },
                   new ShopItem
                   {
                       Id = 4,
                       Price = 10000.0M,
                       QuantityInStock = 22,
                   }
                );

            modelBuilder.Entity<Product>().HasData
                (
                new Product
                {
                    Id = 1,
                    ItemId = 1,
                    Name = "دوره آموزشی ASP.NET MVC پیشرفته",
                    Description = "در این دوره به مباحث پیشرفته ASP.NET MVC نیز خواهیم پرداخت"
                }, new Product
                {
                    Id = 2,
                    ItemId = 2,
                    Name = "دوره آموزشی ASP.NET Core ",
                    Description = "در این دوره به آموزش ASP.NET Core خواهیم پرداخت"
                },
                 new Product
                 {
                     Id = 3,
                     ItemId = 3,
                     Name = "لباس غواصی",
                     Description = "این لباس غواصی در برابر حملات کوسه ها مقاوم بوده و جان شما را حفظ میکند"
                 },
                  new Product
                  {
                      Id = 4,
                      ItemId = 4,
                      Name = "ساعت مچی رولکس",
                      Description = "این ساعت از جواهرات و الماس تزئین شده است"
                  }
                );

            modelBuilder.Entity<CategoryToProduct>().HasData
               (
                new CategoryToProduct() { CategoryId = 2, ProductId = 1 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 1 },
                new CategoryToProduct() { CategoryId = 4, ProductId = 1 },
                new CategoryToProduct() { CategoryId = 5, ProductId = 1 },
                new CategoryToProduct() { CategoryId = 2, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 4, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 5, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 2, ProductId = 3 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 3 },
                new CategoryToProduct() { CategoryId = 4, ProductId = 3 },
                new CategoryToProduct() { CategoryId = 5, ProductId = 3 },
                new CategoryToProduct() { CategoryId = 2, ProductId = 4 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 4 },
                new CategoryToProduct() { CategoryId = 4, ProductId = 4 },
                new CategoryToProduct() { CategoryId = 5, ProductId = 4 }



               );
            #endregion 
            base.OnModelCreating(modelBuilder);
        }
    }
}
