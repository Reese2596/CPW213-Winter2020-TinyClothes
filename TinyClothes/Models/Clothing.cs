using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    /// <summary>
    /// Represents a single clothing Item(ex: Shirt)
    /// </summary>
    public class Clothing
    {
        /// <summary>
        /// The unique identifier for the clothing item
        /// </summary>
        [Key] //Set as primary key when using Entity Framework
        public int ItemID { get; set; }

        /// <summary>
        /// The size of the clothing objects (ex: S, M, L)
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// The type of clothing shirt, pants, skirt, etc.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Color of the clothing Item
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Retail price?(ex: 9.99)
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// The title of the products 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// a brief description og the clothes
        /// </summary>
        public string Description { get; set; }
    }
}
