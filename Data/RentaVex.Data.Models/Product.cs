﻿namespace RentaVex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using RentaVex.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public Product()
        {
            this.Images = new HashSet<Image>();
            this.Availabilities = new HashSet<ProductAvailability>();
            this.UserInteractions = new HashSet<UserInteraction>();

        }
                
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Location { get; set; }

        public string Contact { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        // [ForeignKey("User")] -->> Not sure if it should be int or string
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual Category Category { get; set; }

        public bool IsForSale { get; set; }

        public bool IsForRent { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<ProductAvailability> Availabilities { get; set; }

        public DateTime PickupTime { get; set; }

        public DateTime ReturnTime { get; set; }

        public bool CourierDelivery { get; set; }

        public ConditionType Condition { get; set; }

        public bool IsWarned { get; set; }

        public string WarningMessage { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ProductRating ProductRating { get; set; }

        public ICollection<UserInteraction> UserInteractions { get; set; }
    }
}
