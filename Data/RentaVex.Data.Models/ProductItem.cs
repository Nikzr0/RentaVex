namespace RentaVex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RentaVex.Data.Common.Models;

    // Camping equipement -->> (tent, sleeping bag and etc.)
    public class ProductItem
    {
        public int ProductItemID { get; set; }

        public int ProductID { get; set; }

        public string ItemName { get; set; }

        public string Description { get; set; }

        public decimal ItemPrice { get; set; }

        public ConditionType ItemCondition { get; set; }

        public Product Product { get; set; }

        public ICollection<UserInteraction> UserInteractions { get; set; }

        public ICollection<Like> Likes { get; set; } // Likes for this item -->> (no idea if we need it)
    }
}
