namespace RentaVex.Web.ViewModels.Products
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RatingViewModel
    {
        [Range(0, 6)]
        public int NumberOfStars { get; set; }

        public int ProductId { get; set; }

        public double AverageRating { get; set; }
    }
}
