namespace RentaVex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    // No idea if I actually need this table :)
    public class ProductCategory
    {
        public int ProductID { get; set; }

        public int CategoryID { get; set; }

        public Product Product { get; set; }

        public Category Category { get; set; }
    }
}
