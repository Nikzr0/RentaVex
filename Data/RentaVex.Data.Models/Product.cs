namespace RentaVex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RentaVex.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Location { get; set; }

        public string Contact { get; set; }

        public int CategoryID { get; set; } // Foreign key to Category

        public virtual Category Category { get; set; } // Navigation property


        public bool IsForSale { get; set; }

        public bool IsForRent { get; set; }


        // It will only work if the images are from the internet, but because real users will use the web app they will upload and we cannot do it like this.
        // public ICollection<string> Photos { get; set; } // Collection of photo URLs
        public ICollection<Image> Images { get; set; } // Collection of images

        public ICollection<ProductAvailability> Availabilities { get; set; } // must figure it out

        public ICollection<ProductCategory> ProductCategories { get; set; } // Many-to-many relationship/ No idea :)

        //public ICollection<Category> Categories => ProductCategories.Select(pc => pc.Category).ToList(); // Convenience property for easy access to categories

        public DateTime PickupTime { get; set; }

        public DateTime ReturnTime { get; set; }

        public bool CourierDelivery { get; set; }

        public ConditionType Condition { get; set; } // Enum for condition (used, new, etc.)

        public ProductRating ProductRating { get; set; } // Average rating from user interactions

        public ICollection<ProductItem> ProductItems { get; set; } // Collection of items composing the product

        public bool IsWarned { get; set; }

        public string WarningMessage { get; set; }

        public ICollection<UserInteraction> UserInteractions { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public ICollection<Like> Likes { get; set; }
    }
}
