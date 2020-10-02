using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using p1.Models;

namespace p1.Data
{
    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        public AuthContext(DbContextOptions<AuthContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Store>().HasData(
                new Store
                {
                    Id = 1,
                    StoreName = "Trader Joe",
                    Street = "123 Main Street",
                    City = "New York City",
                    State = "NY",
                    Zip = "12345"



                },

               new Store
               {
                   Id = 2,
                   StoreName = "Target",
                   Street = "321 Main Street",
                   City = "Jersey City",
                   State = "New Jersey",
                   Zip = "12346"

               },

               new Store
               {
                   Id = 3,
                   StoreName = "Costco",
                   Street = "456 Main Street",
                   City = "Jacksonville",
                   State = "Florida",
                   Zip = "12346"

               },

               new Store
               {
                   Id = 4,
                   StoreName = "Walmart",
                   Street = "654 Main Street",
                   City = "Boulder",
                   State = "Colorado",
                   Zip = "12346"
               }




               );

            modelBuilder.Entity<Product>().HasData(
                 new Product
                 {
                     ProductId = 1,
                     ProductName = "Toilet Paper",
                     Price = 100.00,
                     ProductDescription = "white gold"




                 },
                 new Product
                 {
                     ProductId = 2,
                     ProductName = "Hand sanitizer",
                     Price = 101.00,
                     ProductDescription = "liquid gold"

                 },

                  new Product
                  {
                      ProductId = 3,
                      ProductName = "Mask",
                      Price = 102.00,
                      ProductDescription = "fabric gold"

                  },

                     new Product
                     {
                         ProductId = 4,
                         ProductName = "Clorox Wipes",
                         Price = 500.00,
                         ProductDescription = "wiping gold"

                     }

                );

            modelBuilder.Entity<Inventory>().HasData(
              new Inventory
              {


                  InventoryId = 1,
                  StoreId = 1,
                  ProductId = 1,
                  Quantity = 10

              },

              new Inventory
              {
                  InventoryId = 2,
                  StoreId = 1,
                  ProductId = 2,
                  Quantity = 20
              },

                new Inventory
                {
                    InventoryId = 3,
                    StoreId = 1,
                    ProductId = 3,
                    Quantity = 30
                },

                     new Inventory
                     {
                         InventoryId = 4,
                         StoreId = 1,
                         ProductId = 4,
                         Quantity = 40
                     },

                      new Inventory
                      {
                          InventoryId = 5,
                          StoreId = 2,
                          ProductId = 1,
                          Quantity = 10000
                      },

                new Inventory
                {
                    InventoryId = 6,
                    StoreId = 2,
                    ProductId = 2,
                    Quantity = 1000
                },

                new Inventory
                {
                    InventoryId = 7,
                    StoreId = 2,
                    ProductId = 3,
                    Quantity = 132
                },
                new Inventory
                {
                    InventoryId = 8,
                    StoreId = 2,
                    ProductId = 4,
                    Quantity = 1120
                },

                new Inventory
                {
                    InventoryId = 9,
                    StoreId = 3,
                    ProductId = 1,
                    Quantity = 123
                }

                , new Inventory
                {
                    InventoryId = 10,
                    StoreId = 3,
                    ProductId = 4,
                    Quantity = 120
                },

                 new Inventory
                 {
                     InventoryId = 11,
                     StoreId = 4,
                     ProductId = 2,
                     Quantity = 1200
                 },

                      new Inventory
                      {
                          InventoryId = 12,
                          StoreId = 4,
                          ProductId = 1,
                          Quantity = 1
                      },


                      new Inventory
                      {
                          InventoryId = 13,
                          StoreId = 4,
                          ProductId = 3,
                          Quantity = 0
                      }



             );


        }

    }


        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Inventory> Inventory { get; set; }

        
    }
}
