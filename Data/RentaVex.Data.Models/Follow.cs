namespace RentaVex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Follow
    {
        public int FollowID { get; set; } // Do I even need that

        public int FollowerID { get; set; }

        public int SellerID { get; set; }

        // Add other follow-related properties as needed

        // Navigation properties
        public User Follower { get; set; }

        public User Seller { get; set; }
    }
}
