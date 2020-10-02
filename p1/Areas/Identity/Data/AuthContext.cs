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
                    StoreId = 1,
                    StoreName = "Trader Joe",
                    Street = "123 Main Street",
                    City = "New York City",
                    State = "NY",
                    Zip = "12345"



                },

               new Store
               {
                   StoreId = 2,
                   StoreName = "Target",
                   Street = "321 Main Street",
                   City = "Jersey City",
                   State = "New Jersey",
                   Zip = "12346"

               },

               new Store
               {
                   StoreId = 3,
                   StoreName = "Costco",
                   Street = "456 Main Street",
                   City = "Jacksonville",
                   State = "Florida",
                   Zip = "12346"

               },

               new Store
               {
                   StoreId = 4,
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
                     Name = "Toilet Paper",
                     Price = 100.00,





                 },
                 new Product
                 {
                     ProductId = 2,
                     Name = "Hand sanitizer",
                     Price = 101.00,


                 },

                  new Product
                  {
                      ProductId = 3,
                      Name = "Mask",
                      Price = 102.00,


                  },

                     new Product
                     {
                         ProductId = 4,
                         Name = "Clorox Wipes",
                         Price = 500.00,


                     }

                );

            modelBuilder.Entity<Inventory>().HasData(
              new Inventory
              {


                  InventoryId = 1,


              },

              new Inventory
              {
                  InventoryId = 2,

              },

                new Inventory
                {
                    InventoryId = 3,

                },

                     new Inventory
                     {
                         InventoryId = 4,

                     },

                      new Inventory
                      {
                          InventoryId = 5,

                      },

                new Inventory
                {
                    InventoryId = 6,

                },

                new Inventory
                {
                    InventoryId = 7,

                },
                new Inventory
                {
                    InventoryId = 8,

                },

                new Inventory
                {
                    InventoryId = 9,

                }

                , new Inventory
                {
                    InventoryId = 10,


                },

                 new Inventory
                 {
                     InventoryId = 11,

                 },

                      new Inventory
                      {
                          InventoryId = 12,

                      },


                      new Inventory
                      {
                          InventoryId = 13,

                      }



             );


        }




        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Inventory> Inventory { get; set; }

    }
        
    
}
