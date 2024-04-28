namespace RentaVex.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using RentaVex.Data.Common.Models;

    public class UnavailableDate : BaseDeletableModel<int>
    {
        public DateTime Date { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
