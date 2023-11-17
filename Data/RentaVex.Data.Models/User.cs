namespace RentaVex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RentaVex.Data.Common.Models;

    public class User : BaseDeletableModel<int>
    {
        public User()
        {
            this.UserInteractions = new HashSet<UserInteraction>();
            this.Transactions = new HashSet<Transaction>();
            this.Likes = new HashSet<Like>();
            this.Products = new HashSet<Product>();
            this.Followers = new HashSet<Follow>();
            this.Following = new HashSet<Follow>();
            this.Sellers = new HashSet<Follow>();
            this.LikedProducts = new HashSet<Product>();
        }

        // public int UserID { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public ICollection<UserInteraction> UserInteractions { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Product> Products { get; set; }

        public ICollection<Follow> Followers { get; set; }

        public ICollection<Follow> Following { get; set; }

        public ICollection<Follow> Sellers { get; set; }

        public ICollection<Product> LikedProducts { get; set; }
    }
}
