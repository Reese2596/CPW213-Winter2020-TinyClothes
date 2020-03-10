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
        public static async Task<int> GetNumClothing(StoreContext context)
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

        /// <summary>
        /// Changes an individual clothing item in the database 
        /// and save said changes.
        /// </summary>
        /// <param name="c">Clothing object</param>
        /// <param name="context">DBcontext</param>
        public static async Task<Clothing> Edit(Clothing c, StoreContext context)
        {
            await context.AddAsync(c);
            context.Entry(c).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return c;
        }

        /// <summary>
        /// a single clothing item or null if there are no matches.
        /// </summary>
        /// <param name="id">The Id of the item</param>
        /// <param name="context">DB context</param>
        public static async Task<Clothing> GetClothingByID(int id, StoreContext context)
        {
            Clothing c =
                await (from clothing in context.Clothing
                        where clothing.ItemID == id
                        select clothing).SingleOrDefaultAsync();
            return c;
        }

        /// <summary>
        /// Overload the previous Delete method.
        /// Deletes single clothing item by id.
        /// </summary>
        /// <param name="c">Clothing object</param>
        /// <param name="context">DB Context</param>
        public static async Task Delete(Clothing c, StoreContext context)
        {
            await context.AddAsync(c);
            context.Entry(c).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks that a search criteria is used. Return the Clothes that are true to 
        /// the search.
        /// </summary>
        /// <param name="search"></param>
        public static async Task<SearchCriteria> BuildSearchQuery(SearchCriteria search, StoreContext context)
        {
            IQueryable<Clothing> allCothes = from c in context.Clothing
                                             select c;
            //Where minPrice < ?
            if (search.MinPrice.HasValue)
            {
                allCothes = from c in allCothes
                            where c.Price >= search.MinPrice
                            select c;
            }

            //Where Price < MaxPrice
            if (search.MaxPrice.HasValue)
            {
                allCothes = from c in allCothes
                            where c.Price <= search.MaxPrice
                            select c;
            }

            if (!string.IsNullOrWhiteSpace(search.Size))
            {
                allCothes = from c in allCothes
                            where c.Size == search.Size
                            select c;
            }

            if (!string.IsNullOrWhiteSpace(search.Type))
            {
                allCothes = from c in allCothes
                            where c.Type.Contains(search.Type)
                            select c;
            }

            if (!string.IsNullOrWhiteSpace(search.Title))
            {
                allCothes = from c in allCothes
                            where c.Title.Contains(search.Title)
                            select c;
            }

            search.Results = await allCothes.ToListAsync();
            return search;
        }
    }
}
