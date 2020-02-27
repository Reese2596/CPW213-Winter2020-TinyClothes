using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    /// <summary>
    /// Helper class to manage user shopping cart data using cookies.
    /// </summary>
    public static class CartHelper
    {
        /// <summary>
        /// The Key used to access and add data to Cookie
        /// </summary>
        private const string CartCookie = "CartCookie";

        /// <summary>
        /// Add a single clothing object to class
        /// </summary>
        /// <param name="c"> A single clothing object.</param>
        /// <param name="http"> An accessor to get to cookies.</param>
        public static void Add(Clothing c, IHttpContextAccessor http)
        {
            //Grab all existing clothing items
            List<Clothing> clothes = GetAllClothingItems(http);
            clothes.Add(c);

            //Convert the clothing object into the string representaion.
            string data = JsonConvert.SerializeObject(clothes);
            //Create Cookie Options 
            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(14),
                IsEssential = true,
                Secure = true
            };

            //Create Cookie to Store string Representaion and add Cookie.
            http.HttpContext.Response.Cookies.Append(CartCookie, data, options);

        }

        //Needs TO DO: refractor to use multiple inputs into cookie
        /// <summary>
        /// Grabs the total number of items in the cart.
        /// Using the data stored in cookie.
        /// </summary>
        /// <param name="http">Used to get access to the cookies</param>
        public static int GetCount(IHttpContextAccessor http)
        {
            List<Clothing> allClothes = GetAllClothingItems(http);
            return allClothes.Count;
        }

        /// <summary>
        /// Grab all of the clothing from cart(Cookie) and displays them in an List of type Clothing.
        /// If no items present empty list returned. Using data stored in Cookie.
        /// </summary>
        /// <param name="http">Used to get access to the cookies</param>
        public static List<Clothing> GetAllClothingItems(IHttpContextAccessor http)
        {
            string data = http.HttpContext.Request.Cookies[CartCookie];
            if (string.IsNullOrWhiteSpace(data))
            {
                return new List<Clothing>();
            }
            else
            {
                return JsonConvert.DeserializeObject<List<Clothing>>(data);
            }
        }
    }
}
