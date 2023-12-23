namespace RentaVex.Web.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using RentaVex.Data.Models;

    public class CreateProducViewModel
    {
        public CreateProducViewModel()
        {
            this.PickupTime = DateTime.UtcNow.Date.AddDays(1);
            this.ReturnTime = DateTime.UtcNow.Date.AddDays(1);
        }

        [Required(ErrorMessage = "The product name is required.")]
        [MaxLength(35, ErrorMessage = "The product name must be no more than 35 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A description is required.")]
        [MaxLength(1800, ErrorMessage = "The description must be no more than 1800 characters.")]
        public string Description { get; set; }

        public bool IsForRent { get; set; }

        public bool IsForSale { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The price must be a non-negative value.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The location is required.")]
        public string Location { get; set; }

        public string Contact { get; set; }

        public int CategoryId { get; set; }

        public DateTime PickupTime { get; set; }

        public DateTime ReturnTime { get; set; }

        public Category Category { get; set; }

        public bool CourierDelivery { get; set; }

        [Required(ErrorMessage = "At least one image is required.")]
        public IEnumerable<IFormFile> Images { get; set; }

        public ConditionType Condition { get; set; }

        public bool IsWarned { get; set; }

        public string WarningMessage { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }
    }
}
