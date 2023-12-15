namespace RentaVex.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using RentaVex.Data.Common.Models;
    using System.Collections.Generic;

    public class User : IdentityUser<int>
    {
        public User()
        {
            this.UserInteractions = new HashSet<UserInteraction>();
            this.Likes = new HashSet<Like>();
            this.LikedProducts = new HashSet<Product>();
        }

        public ICollection<UserInteraction> UserInteractions { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Product> LikedProducts { get; set; }
    }
}
