using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinyClothes.Data;
using TinyClothes.Models;

namespace TinyClothes.Controllers
{
    public class CartController : Controller
    {
        private readonly StoreContext _context;
        private readonly IHttpContextAccessor _http;
        public CartController(StoreContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }

        /// <summary>
        /// Display all product in cart.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Adds a single object to the cart.
        /// Quantity does not matter if same object added 5 times displayed 5 times.
        /// </summary>
        public async Task<IActionResult> AddToCart(int id)
        {
            //Grab the clothing Object by Id.
            Clothing c = await ClothingDb.GetClothingByID(id, _context);
            //Add Clothing object to be stored in cookie.
            if(c != null)
            {
                CartHelper.Add(c, _http);
            }
            return View();
        }

        /// <summary>
        /// Summary/Checkout page.
        /// </summary>
        public IActionResult CheckOut()
        {
            return View();
        }
    }
}