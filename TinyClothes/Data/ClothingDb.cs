using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyClothes.Models;

namespace TinyClothes.Data
{
    /// <summary>
    /// Contains DB helper methods for <see cref="Models.Clothing"/>
    /// </summary>
    public static class ClothingDb
    {
        public static List<Clothing> GetAllClothing()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a clothing object to the database 
        /// returns the object with ID populated.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static async Task<Clothing> Add(StoreContext context, Clothing c)
        {
            await context.AddAsync(c); //prepares insert query
            await context.SaveChangesAsync(); //execute the insert query

            return c;
        }
    }
}
