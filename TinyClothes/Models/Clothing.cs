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
        [Required(ErrorMessage = "Size Requiured ex(one Size, Small, Medium, etc.)")]
        [StringLength(15)]
        public string Size { get; set; }

        /// <summary>
        /// The type of clothing shirt, pants, skirt, wheelchair, etc.
        /// </summary>
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// Color of the clothing Item
        /// </summary>
        [Required]
        public string Color { get; set; }

        /// <summary>
        /// Retail price?(ex: 9.99)
        /// </summary>
        [Range(0.99, 5000)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        /// <summary>
        /// The title of the products 
        /// </summary>
        [Required]
        [StringLength(35)]
        //Sample Regular Expression(Regex) great for validation.
        //[RegularExpression("^([A-Za-z])+$")]
        public string Title { get; set; }

        /// <summary>
        /// a brief description og the clothes
        /// </summary>
        [StringLength(800)]       
        public string Description { get; set; }
    }
}
