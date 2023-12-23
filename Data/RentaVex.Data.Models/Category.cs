namespace RentaVex.Data.Models
{
    using System.Collections.Generic;

    using RentaVex.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
