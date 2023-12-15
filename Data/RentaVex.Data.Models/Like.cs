namespace RentaVex.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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
