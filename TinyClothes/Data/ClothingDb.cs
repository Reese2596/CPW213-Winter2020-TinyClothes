using Microsoft.EntityFrameworkCore;
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
        /// <summary>
        /// returns the total nuber of clothing items
        /// </summary>
        /// <returns></returns>
        public async static Task<int> GetNumClothing(StoreContext context)
        {
            return await context.Clothing.CountAsync();
            #region Query Syntax
            //return await (from c in context.Clothing
            //              select c).CountAsync();
            #endregion
        }

        /// <summary>
        /// Returns specific page of clothing item s sorted by item id  in asc order.
        /// </summary>
        /// <param name="pageNum">Product page #</param>
        /// <param name="pageSize">Clothing items per page</param>
        public static async Task<List<Clothing> > GetClothingByPage(StoreContext context
            , int pageNum, int pageSize)
        {
            //if you want page one you would not need to skip rows so 
            //must offset by one to get correct data.
            const int pageOffset = 1;

            //LINQ Method syntax
            List<Clothing> clothes =
                await context.Clothing
                        .Skip(pageSize * (pageNum - pageOffset))    //Must do skip than take
                        .Take(pageSize)
                        .OrderBy(c => c.ItemID)
                        .ToListAsync();
            #region LINQ Query Syntax
            //List<Clothing> clothings =
            //     await (from c in context.Clothing
            //            orderby c.ItemID ascending
            //            select c).Skip(pageSize * (pageNum - pageOffset))    //Must do skip than take
            //                     .Take(pageSize)
            //                     .ToListAsync();
            #endregion

            return clothes;
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
