namespace RentaVex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Like
    {
        public int LikeID { get; set; }

        public int UserID { get; set; }

        public int ProductID { get; set; } // Navigation properties

        public User User { get; set; }

        public Product Product { get; set; }
    }
}
