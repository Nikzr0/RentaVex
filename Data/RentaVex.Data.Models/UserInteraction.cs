namespace RentaVex.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserInteraction
    {
        public int UserInteractionID { get; set; }

        public int UserID { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }

        public string Comment { get; set; }

        public bool HasWarning { get; set; }

        public string WarningMessage { get; set; }

        public ProductRating Rating { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }
    }
}
