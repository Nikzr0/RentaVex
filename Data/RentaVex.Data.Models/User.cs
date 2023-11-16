namespace RentaVex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class User
    {
        public int UserID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        // Add other user-related properties as needed

        // Navigation properties
        public ICollection<UserInteraction> UserInteractions { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Product> Products { get; set; } // Products added by the user

        public ICollection<Follow> Followers { get; set; }

        public ICollection<Follow> Following { get; set; }

        public ICollection<Follow> Sellers { get; set; }

        public ICollection<Product> LikedProducts { get; set; } // Collection of liked products
    }
}
