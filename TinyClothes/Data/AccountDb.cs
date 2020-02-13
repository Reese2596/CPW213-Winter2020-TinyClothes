using System;
using System.Threading.Tasks;
using TinyClothes.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TinyClothes.Models;

namespace TinyClothes
{
    public static class AccountDb
    {
        /// <summary>
        /// Checks if the Username is taken or if it is available.
        /// </summary>
        /// <param name="userName">Username to check</param>
        /// <param name="context">The DB context</param>
        public static async Task<bool> IsUserNameTaken(string userName, StoreContext context)
        {
            bool isTaken = await (from acc in context.Accounts
                                where acc.Username == userName
                                select acc).AnyAsync();
            return isTaken;
        }

        /// <summary>
        /// Adds a single Account to the Database.
        /// </summary>
        /// <param name="acc">Account object</param>
        /// <param name="context">DB Context</param>
        public static async Task<Account> Register(Account acc, StoreContext context)
        {
            await context.Accounts.AddAsync(acc);
            await context.SaveChangesAsync();
            return acc;
        }
        
    }
}