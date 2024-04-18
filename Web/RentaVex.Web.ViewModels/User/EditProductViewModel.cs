namespace RentaVex.Web.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using RentaVex.Data.Models;

    public class EditProductViewModel
    {
        [MaxLength(35, ErrorMessage = "The product name must be no more than 35 characters.")]
        public string Name { get; set; }

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

        public Category Category { get; set; }

        public bool CourierDelivery { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }

        public ConditionType Condition { get; set; }

        public bool IsWarned { get; set; }

        public string WarningMessage { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }
    }
}