namespace RentaVex.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UnavailableDate
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
