namespace RentaVex.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ProductUnavailableDates
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        // [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public DateTime UnavailableDate { get; set; }
    }
}
