namespace RentaVex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using RentaVex.Data.Common.Models;

    public class Like
    {
        [Key]
        public int LikeID { get; set; }

        [ForeignKey("ProductID")]
        public int UserId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }
    }
}
