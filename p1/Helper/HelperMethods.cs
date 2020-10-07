using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using p1.Data;
using p1.Helpers;
using p1.Models;
using Xunit.Sdk;


namespace p1.Helper
{
    public class HelperMethods
    {
        public static List<Store> ReturnStores(AuthContext _context)
        {
            var stores = _context.Store.ToList();
            return stores;
        }

        public static Store ReturnStoreById(AuthContext _context, int id)
        {
            var store = _context.Store.Find(id);
            return store;
        }

        public static List<Inventory> ReturnInventoryById(AuthContext _context, int id)
        {
            var inventory = _context.Inventory.Where(b => (b.StoreId == id) && (b.Quantity > 0)).Include(m => m.Product).ToList();
            return inventory;
        }


        public static List<Product> ReturnProductById(AuthContext _context, int id)
        {
            var product = _context.Product.Where(b => b.ProductId == id).ToList();
            return product;
        }

        public static List<Order> ReturnOrderByCustomer(AuthContext _context, ApplicationUser user)
        {
            var userid = user.Id;
            var orders = _context.Order.Where(b => (b.ApplicationUser.Id == userid)).Include(m => m.Details).ToList();

            return orders;
        }

        public static List<OrderDetails> ReturnOrderDetailsByOrderId (AuthContext _context, int id)
        {
            List<OrderDetails> stuff1 = _context.OrderDetails.Where(b => (b.Order.OrderId == id)).Include(m => m.Product).ToList();
            return stuff1;
        }

        public static List<Order> ReturnStoreOrdersByStoreId(AuthContext _context, int id)
        {
            List<Order> orders = _context.Order.Where(b => (b.StoreId == id)).Include(m => m.Details).Include(z => z.ApplicationUser).ToList();
            return orders;
        }

        public static List <Order> ReturnCustomerOrderByEmail(AuthContext _context, string email)
        {
            List<Order> orders = _context.Order.Where(b => (b.ApplicationUser.Email == email)).ToList();
            return orders;
        }

        public static List<ApplicationUser> ReturnCustomerByEmail(AuthContext _context, string email)
        {
            List<ApplicationUser> user = _context.ApplicationUsers.Where(b => (b.Email == email)).ToList();
            return user;
        }

        public static List<ApplicationUser> ReturnAllCustomer(AuthContext _context)
        {
            List<ApplicationUser> user = _context.ApplicationUsers.ToList();
            return user;
        }

        public static List<Inventory> ReturnAllInventory(AuthContext _context)
        {
            List<Inventory> inv = _context.Inventory.ToList();
            return inv;
        }
        public static List<Product> ReturnAllProduct(AuthContext _context)
        {
            List<Product> prod = _context.Product.ToList();
            return prod;
        }

       




    }
}
