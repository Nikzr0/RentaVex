namespace RentaVex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserInteraction
    {
        public int UserInteractionID { get; set; }

        public int UserID { get; set; }

        public int ProductID { get; set; }

        // public InteractionType Type { get; set; } enum for like -->> (no idea how it works)
        public ProductRating Rating { get; set; } // Rating from 0 to 5

        public string Comment { get; set; }

        public bool HasWarning { get; set; }

        public string WarningMessage { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }
    }
}
