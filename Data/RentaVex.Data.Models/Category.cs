namespace RentaVex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Category
    {
        public int CategoryID { get; set; }

        public string Name { get; set; }

        //public int? ParentCategoryID { get; set; }
        
        public virtual Category ParentCategory { get; set; }

        public virtual ICollection<Category> Subcategories { get; set; } // Collection of subcategories

        public ICollection<ProductCategory> ProductCategories { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
