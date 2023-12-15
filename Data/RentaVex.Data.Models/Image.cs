namespace RentaVex.Data.Models
{
    using RentaVex.Data.Common.Models;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string AddedByUser { get; set; }

        public string ImageUrl { get; set; }

        // Image type -->> (.png or etc.)
        public string Extention { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
