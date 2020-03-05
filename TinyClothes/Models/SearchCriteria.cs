using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    public class SearchCriteria
    {
        public SearchCriteria()
        {
            Results = new List<Clothing>();
        }
        public string Size { get; set; }

        /// <summary>
        /// The type of clothing(Shirt/Pants/Hat/Cane/WheelChair/etc.
        /// </summary>
        public string Type { get; set; }

        [StringLength(150)]
        public string Title { get; set; }

        [Range(0.00, double.MaxValue, ErrorMessage = "Min Price, Must be Positive.")]
        [Display(Name = "Min Price")]
        public double? MinPrice { get; set; }

        [Range(0.00, double.MaxValue, ErrorMessage = "Max Price, Must be Positive.")]
        [Display(Name = "Max Price")]
        public double? MaxPrice { get; set; }

        public List<Clothing> Results { get; set; }

        /// <summary>
        /// Checks and returns true if any one of the criteria is provided/being searched.
        /// </summary>
        public bool IsBeingSearched()
        {
            return !string.IsNullOrWhiteSpace(Size) || !string.IsNullOrWhiteSpace(Type) ||
                   !string.IsNullOrWhiteSpace(Title) || MinPrice.HasValue
                   || MaxPrice.HasValue;         
        }
    }
}
