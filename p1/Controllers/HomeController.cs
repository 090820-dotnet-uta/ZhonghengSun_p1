using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using p1.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using p1.Data;
using p1.Helpers;
using p1.Models;
using Xunit.Sdk;

namespace p1.Controllers { 
[Authorize]
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AuthContext _context;
        private UserManager<ApplicationUser> _userManager;
     
        public HomeController(ILogger<HomeController> logger, AuthContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var view = HelperMethods.ReturnStores(_context);
            return View(view);
        }

      public IActionResult Store(int id)
        {
            if (SessionHelper.GetObjectFromJson<List<Store>>(HttpContext.Session, "store") == null){
                List<Store> stores = new List<Store>();
                Store store = new Store();
                store = HelperMethods.ReturnStoreById(_context, id);
                stores.Add(store);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "store", stores);

            }
            else
            {
                var stores = SessionHelper.GetObjectFromJson<List<Store>>(HttpContext.Session, "store");
                Store store = new Store();
                store = HelperMethods.ReturnStoreById(_context, id);
                stores.Add(store);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "store", stores);

            }

         




            List<Inventory> inventory = HelperMethods.ReturnInventoryById(_context, id);

            return View(inventory);


        }


      public IActionResult Checkout()
        {
          //  orderD.Quantity = 0;
            List<String> prodList = new List<String>();
            Order order = new Order();
            //OrderDetails orderD = new OrderDetails();

            var store = SessionHelper.GetObjectFromJson<List<Store>>(HttpContext.Session, "store");
            var cart = SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart");
            
            Store lastStore = store.Last();
            var inventory = HelperMethods.ReturnInventoryById(_context, lastStore.StoreId);
            
            int numprod = inventory.Count();
            var user = FindUser().Result;
            order.ApplicationUser = user;

            order.OrderTime = DateTime.Now;
            try
            {
                order.totalamount = cart.Sum(item => item.Product.Price * item.Quantity);
            } catch (ArgumentNullException ex)
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
            order.StoreId = lastStore.StoreId;
            _context.Order.Add(order);
            

            foreach (var item in cart)
            {

                OrderDetails orderD = new OrderDetails();
                //  prodList.Add(item.Product.Name);
                var product = HelperMethods.ReturnProductById(_context, item.Product.ProductId);
                orderD.Product = product.Last();
                orderD.Quantity = item.Quantity;
                orderD.productNames = prodList.ToArray();
                orderD.Order = order;
                _context.Add(orderD);
                _context.SaveChanges();
                for (int p = 0; p < numprod; p++) {
                    if (item.Product.ProductId == inventory[p].Product.ProductId)
                    {
                        inventory[p].Quantity = inventory[p].Quantity - item.Quantity;
                        _context.SaveChanges();
                      

                    }

                }
            }
            //orderD.productNames = prodList.ToArray();
            //order.Details = orderD;
           // _context.OrderDetails.Add(orderD);
          

            return RedirectToAction("OrdersMade");
        }

        public IActionResult Orders()
        {
            var user = FindUser().Result;
           
            List<List<OrderDetails>> stuff2 = new List<List<OrderDetails>>();
            List<Order> orders = HelperMethods.ReturnOrderByCustomer(_context, user);
            foreach (var order in orders)
            {
                List<OrderDetails> stuff1 = HelperMethods.ReturnOrderDetailsByOrderId(_context, order.OrderId);
                stuff2.Add(stuff1);


            }
            return View(stuff2);


        }

        public IActionResult OrdersMade()
        {
            var user = FindUser().Result;
            //  List<Order> order = _context.Order.Where(b => (b.ApplicationUser.Id == user.Id)).Include(m => m.Details).ToList();
            List<Order> order = HelperMethods.ReturnOrderByCustomer(_context, user);
            var stuff = order.Last();
            List <OrderDetails> stuff1 = HelperMethods.ReturnOrderDetailsByOrderId(_context, stuff.OrderId);

            return View(stuff1);


        }

        public IActionResult StoreOrders(int id)
        {

            List<Order> orders = HelperMethods.ReturnStoreOrdersByStoreId(_context, id);
            return View(orders);
        }


        public IActionResult UserOrders(string email)
        {
            List<Order> orders = HelperMethods.ReturnCustomerOrderByEmail(_context, email);
            return View(orders);
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult SearchPage()
        {
            var email = Request.Form["test"].First();
            //if (email == null)
            //{
            //    return View("Index");
            //}
            List<ApplicationUser> user = HelperMethods.ReturnCustomerByEmail(_context, email);
            if (user.Count == 0)
            {
                return View("ErrorEmail");
            }
            return View(user);

        }



        public IActionResult Clear()
        {
            
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }




        public async Task<ApplicationUser> FindUser()
        {
            var user = await _userManager.GetUserAsync(User);
            return user;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
