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
            this.UnavailableDates = new HashSet<UnavailableDate>();
            this.ProductRatings = new HashSet<ProductRating>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Location { get; set; }

        public string Contact { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public string UserId { get; set; }

        public virtual Category Category { get; set; }

        public bool IsForSale { get; set; }

        public bool IsForRent { get; set; }

        public ICollection<Image> Images { get; set; }

        public bool CourierDelivery { get; set; }

        public ConditionType Condition { get; set; }

        public bool IsWarned { get; set; }

        public string WarningMessage { get; set; }

        public int NumberOfLikes { get; set; }

        public double AverageRating { get; set; }

        public virtual ApplicationUser ProductOwner { get; set; }

        public virtual ICollection<ApplicationUser> UserLikes { get; set; }

        public virtual ICollection<ProductRating> ProductRatings { get; set; }

        public ICollection<UnavailableDate> UnavailableDates { get; set; }
    }
}
