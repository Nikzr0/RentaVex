namespace RentaVex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
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

        public ICollection<UserInteraction> UserInteractions { get; set; }

        public void SetUnavailableDates(DateTime pickUpTime, DateTime returnTime)
        {
            this.Availabilities ??= new HashSet<ProductAvailability>();

            // Generate a list of dates between start and end (inclusive)
            var unavailableDates = Enumerable.Range(0, (returnTime - pickUpTime).Days + 1)
                                              .Select(offset => pickUpTime.AddDays(offset))
                                              .ToList();

            this.Availabilities = this.Availabilities
                   .Where(avail => !unavailableDates.Contains(avail.AvailableDate))
                   .ToHashSet();

            foreach (var date in unavailableDates)
            {
                this.Availabilities.Add(new ProductAvailability { AvailableDate = date });
            }
        }
    }
}
