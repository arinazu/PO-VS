using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazine.Core.Models
{
    /// <summary>
    /// Information about Product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Unique Id in system
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Product description from the manufacturer
        /// </summary>
        public string Definition { get; set; }
        
        public string Name { get; set; }
        /// <summary>
        /// Recommended price from the manufacturer
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Product image
        /// </summary>
        public string Image { get; set; }

    }
}
