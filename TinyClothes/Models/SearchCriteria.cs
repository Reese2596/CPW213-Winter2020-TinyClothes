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

        public string Title { get; set; }

        [Range(0.00, 1000.00)]
        public double? MinPrice { get; set; }

        [Range(0.00, double.MaxValue)]
        public double? MaxPrice { get; set; }

        public List<Clothing> Results { get; set; }

    }
}
