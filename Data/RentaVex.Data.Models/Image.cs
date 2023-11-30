namespace RentaVex.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using RentaVex.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Extention { get; set; } // Image type -->> (.png or etc.)

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
