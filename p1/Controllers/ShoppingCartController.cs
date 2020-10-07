using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using p1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using p1.Helpers;
using p1.Data;
using System.Web;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Razor.Language;
using p1.Helper;

namespace p1.Controllers
    
{
    [Route("cart")]
    [Authorize]
    public class CartController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly AuthContext _context;

        public CartController(ILogger<HomeController> logger, AuthContext context)
        {
            _logger = logger;
            _context = context;
        }


        [Route("index")]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            try
            {
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            }
            catch (ArgumentNullException ex)
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return View();
        }
        [HttpPost]
        [Route("buy/{id}")]
        public IActionResult Buy(string id)
        {
            int newId = int.Parse(id);
            int stock = 0;
            var store = SessionHelper.GetObjectFromJson<List<Store>>(HttpContext.Session, "store");
            var currentstore = store.Last();
            var inventory = HelperMethods.ReturnInventoryById(_context, currentstore.StoreId);
            for(int i = 0; i < inventory.Count; i ++)
            {
                if (inventory[i].ProductId == newId)
                {
                    stock = inventory[i].Quantity;
                }
            }

            int x;
            bool isInt = int.TryParse(Request.Form["test"].First(), out x);
            
            if (!isInt || x > stock) { 
                return Redirect(Request.Headers["Referer"].ToString());
            }
            
            x = int.Parse(Request.Form["test"].First());
            
            Product product = new Product();
            if (SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart") == null)
            {
                List<ShoppingCart> cart = new List<ShoppingCart>();
                cart.Add(new ShoppingCart { Product = _context.Product.Find(newId), Quantity = x });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<ShoppingCart> cart = SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity = cart[index].Quantity + x; 
                }
                else
                {
                    cart.Add(new ShoppingCart {ProductId = newId, Product = _context.Product.Find(newId), Quantity = x });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Route("remove/{id}")]
        public IActionResult Remove(string id)
        {
            List<ShoppingCart> cart = SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index + 1);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int isExist(string id)
        {
            List<ShoppingCart> cart = SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

    }
}