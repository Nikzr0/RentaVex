namespace RentaVex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RentaVex.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public Product()
        {
            this.Images = new HashSet<Image>();
            this.Availabilities = new HashSet<ProductAvailability>();
            this.ProductCategories = new HashSet<ProductCategory>();
            this.ProductItems = new HashSet<ProductItem>();
            this.UserInteractions = new HashSet<UserInteraction>();
            this.Transactions = new HashSet<Transaction>();
            this.Likes = new HashSet<Like>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(1800)]
        public string Description { get; set; }

        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        public string Location { get; set; }

        public string Contact { get; set; }

        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public bool IsForSale { get; set; }

        public bool IsForRent { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<ProductAvailability> Availabilities { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }

        public DateTime PickupTime { get; set; }

        public DateTime ReturnTime { get; set; }

        public bool CourierDelivery { get; set; }

        public ConditionType Condition { get; set; }

        public ProductRating ProductRating { get; set; }

        public ICollection<ProductItem> ProductItems { get; set; }

        public bool IsWarned { get; set; }

        public string WarningMessage { get; set; }

        public ICollection<UserInteraction> UserInteractions { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public ICollection<Like> Likes { get; set; }
    }
}
