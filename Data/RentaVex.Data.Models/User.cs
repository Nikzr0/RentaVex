﻿namespace RentaVex.Data.Models
{
    using System.Collections.Generic;

    using RentaVex.Data.Common.Models;

    public class User : BaseDeletableModel<int>
    {
        public User()
        {
            this.UserInteractions = new HashSet<UserInteraction>();
            this.Likes = new HashSet<Like>();
            this.LikedProducts = new HashSet<Product>();
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public ICollection<UserInteraction> UserInteractions { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Product> LikedProducts { get; set; }
    }
}
