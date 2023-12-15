namespace RentaVex.Web.ViewModels.InputModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using RentaVex.Data.Models;

    public class CreateProducInputModel
    {
        public CreateProducInputModel()
        {
            this.PickupTime = DateTime.UtcNow.Date.AddDays(1);

            this.ReturnTime = DateTime.UtcNow.Date.AddDays(1);
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(1800)]
        public string Description { get; set; }

        public bool IsForRent { get; set; }

        public bool IsForSale { get; set; }

        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        public string Location { get; set; }

        public string Contact { get; set; }

        public int CategoryId { get; set; }

        public DateTime PickupTime { get; set; }

        public DateTime ReturnTime { get; set; }

        public Category Category { get; set; }

        public bool CourierDelivery { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }

        public ConditionType Condition { get; set; }

        public bool IsWarned { get; set; }

        public string WarningMessage { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }
    }
}
