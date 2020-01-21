using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyClothes.Models;

namespace TinyClothes.Data
{
    public class StoreContext : DbContext
    {
        /// <summary>
        /// DbContextOptions configures Database
        /// </summary>
        /// <param name="options"></param>
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }

        /// <summary>
        /// Set the Entity to be used by Entity Framework to be added into the Db.
        /// Add DbSet for each Entity.
        /// https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-3.1#create-the-database-context
        /// </summary>
        public DbSet<Clothing> Clothing { get; set; }
    }
}
