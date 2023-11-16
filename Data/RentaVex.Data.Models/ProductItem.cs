namespace RentaVex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RentaVex.Data.Common.Models;

    public class ProductItem 
    {
        public int ProductItemID { get; set; }

        public int ProductID { get; set; }

        public string ItemName { get; set; }

        public string Description { get; set; } // Add a description property for the item

        public decimal ItemPrice { get; set; } // Add a price property for the item

        public ConditionType ItemCondition { get; set; } // Reuse ConditionType for item condition
        // Add other properties for the item as needed

        // Navigation properties
        public Product Product { get; set; }

        public ICollection<UserInteraction> UserInteractions { get; set; } // Users' interactions with this item

        public ICollection<Like> Likes { get; set; } // Likes for this item
    }
}
