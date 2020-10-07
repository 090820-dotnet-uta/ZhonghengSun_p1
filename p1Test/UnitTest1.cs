using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using System.Globalization;
using System.Security.Cryptography;
using System.Xml.Schema;
using System.Media;
using System.IO.Compression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using p1.Models;
using p1.Data;
using Microsoft.EntityFrameworkCore;
using p1;
using p1.Controllers;
using Microsoft.EntityFrameworkCore.Diagnostics;
using p1.Helper;
using Microsoft.AspNetCore.Mvc.Testing;

namespace p1Test
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<p1.Startup>>
    {
        private readonly WebApplicationFactory<p1.Startup> _factory;
        public UnitTest1(WebApplicationFactory<p1.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void TestReturnAllStore()
        {

            var options = new DbContextOptionsBuilder<AuthContext>().UseInMemoryDatabase(databaseName: "TestCart")
                .Options;

            using (var context = new AuthContext(options))
            {
                
                context.Store.Add(new Store { StoreId = 1, StoreName = "Walmart" });

            }

            using (var context = new AuthContext(options))
            {


                var result = HelperMethods.ReturnStores(context);

                Assert.IsType<List<Store>>(result);
            }

    
            }

        [Fact]
        public void TestReturnStoreById()
        {

            var options = new DbContextOptionsBuilder<AuthContext>().UseInMemoryDatabase(databaseName: "TestCart")
                .Options;

            using (var context = new AuthContext(options))
            {
                context.Store.Add(new Store { StoreId = 2 });
                var result = HelperMethods.ReturnStoreById(context, 2);
                Assert.IsType<Store>(result);
            }

            

        }
        [Fact]
        public void TestReturnInventoryById()
        {

            var options = new DbContextOptionsBuilder<AuthContext>().UseInMemoryDatabase(databaseName: "TestCart")
                .Options;

            using (var context = new AuthContext(options))
            {
                context.Inventory.Add(new Inventory { InventoryId = 1 });
            }

            using (var context = new AuthContext(options))
            {
                var result = HelperMethods.ReturnInventoryById(context, 1);
                Assert.IsType<List<Inventory>>(result);
            }


        }
        [Fact]
        public void TestReturnProductById()
        {

            var options = new DbContextOptionsBuilder<AuthContext>().UseInMemoryDatabase(databaseName: "TestCart")
                .Options;

            using (var context = new AuthContext(options))
            {
                context.Product.Add(new Product { ProductId = 1 });
            }

            using (var context = new AuthContext(options))
            {
                var result = HelperMethods.ReturnProductById(context, 1);
                Assert.IsType<List<Product>>(result);
            }


        }

        [Fact]
        public void TestReturnOrderByCust()
        {

            var options = new DbContextOptionsBuilder<AuthContext>().UseInMemoryDatabase(databaseName: "TestCart")
                .Options;

            using (var context = new AuthContext(options))
            {
                ApplicationUser user = new ApplicationUser
                {
                    Id = "1"
                };
                context.Order.Add(new Order { OrderId = 1, ApplicationUser = user});
                context.ApplicationUsers.Add(user);

                var result = HelperMethods.ReturnOrderByCustomer(context, user);
                Assert.IsType<List<Order>>(result);
            }

          

        }

        [Fact]
        public void TestReturnOrderDetailsByOrderId()
        {

            var options = new DbContextOptionsBuilder<AuthContext>().UseInMemoryDatabase(databaseName: "TestCart")
                .Options;

            using (var context = new AuthContext(options))
            {
                Order order = new Order()
                {
                    OrderId = 1
                };
                context.OrderDetails.Add(new OrderDetails { OrderId = 1});
                context.Order.Add(order);

                var result = HelperMethods.ReturnOrderDetailsByOrderId(context, 1);
                Assert.IsType<List<OrderDetails>>(result);
            }

        }
        [Fact]
        public void TestReturnStoreOrderByStoreId()
        {

            var options = new DbContextOptionsBuilder<AuthContext>().UseInMemoryDatabase(databaseName: "TestCart")
                .Options;

            using (var context = new AuthContext(options))
            {
                
                context.Order.Add(new Order { OrderId = 1, StoreId = 1 });
                context.Store.Add(new Store { StoreId = 1 });

                var result = HelperMethods.ReturnStoreOrdersByStoreId(context, 1);
                Assert.IsType<List<Order>>(result);
            }



        }
        [Fact]
        public void TestReturnCustomerOrderByEmail()
        {

            var options = new DbContextOptionsBuilder<AuthContext>().UseInMemoryDatabase(databaseName: "TestCart")
                .Options;

            using (var context = new AuthContext(options))
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = "abc123@gmail.com"
                };

                context.ApplicationUsers.Add(user);
                context.Order.Add(new Order { OrderId = 1, ApplicationUser = user });
                

                var result = HelperMethods.ReturnCustomerOrderByEmail(context, "abc@gmail.com");
                Assert.IsType<List<Order>>(result);
            }



        }

        [Fact]
        public void TestReturnCustomerByEmail()
        {

            var options = new DbContextOptionsBuilder<AuthContext>().UseInMemoryDatabase(databaseName: "TestCart")
                .Options;

            using (var context = new AuthContext(options))
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = "abc123@gmail.com"
                };

                context.ApplicationUsers.Add(user);
            


                var result = HelperMethods.ReturnCustomerByEmail(context, "abc@gmail.com");
                Assert.IsType<List<ApplicationUser>>(result);
            }


        }
        [Fact]
         public void TestReturnInventory()
        {

            var options = new DbContextOptionsBuilder<AuthContext>().UseInMemoryDatabase(databaseName: "TestCart")
                .Options;

            using (var context = new AuthContext(options))
            {
                context.Inventory.Add(new Inventory { InventoryId = 1});



                var result = HelperMethods.ReturnAllInventory(context);
                Assert.IsType<List<Inventory>>(result);
            }


        }
        [Fact]
        public void TestReturnProducts()
        {

            var options = new DbContextOptionsBuilder<AuthContext>().UseInMemoryDatabase(databaseName: "TestCart")
                .Options;

            using (var context = new AuthContext(options))
            {
                context.Product.Add(new Product{ ProductId = 1 });



                var result = HelperMethods.ReturnAllProduct(context);
                Assert.IsType<List<Product>>(result);
            }


        }



        [Theory]
        [InlineData("/")]
        [InlineData("/Home")]
        [InlineData("/Home/Index")]
        [InlineData("/Home/Store")]
        [InlineData("/Home/Orders")]
        [InlineData("/Home/Search")]
        [InlineData("/Home/StoreOrders")]
        [InlineData("/Identity/Account/Manage")]
        [InlineData("/Identity/Account/Manage/Email")]

        public async Task Get_EndpointReturnSuccess (string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers
                .ContentType.ToString());
        }
            

    }


}