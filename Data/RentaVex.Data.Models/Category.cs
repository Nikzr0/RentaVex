namespace RentaVex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RentaVex.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        // public int CategoryID { get; set; }
        public Category()
        {
            this.Subcategories = new HashSet<Category>();
            this.ProductCategories = new HashSet<ProductCategory>();
            this.Products = new HashSet<Product>();
        }

        public string Name { get; set; }

        public virtual Category ParentCategory { get; set; }

        public virtual ICollection<Category> Subcategories { get; set; } // Not sure how it works.

        public ICollection<ProductCategory> ProductCategories { get; set; } // I do not understand ProductCategory

        public virtual ICollection<Product> Products { get; set; }
    }
}
