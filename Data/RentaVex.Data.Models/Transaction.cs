namespace RentaVex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Transaction
    {
        public int TransactionID { get; set; }

        public int UserID { get; set; }

        public int ProductID { get; set; }

        public string TransactionType { get; set; }

        public DateTime TransactionDate { get; set; }

        // Add other transaction-related properties as needed

        // Navigation properties
        public User User { get; set; }

        public Product Product { get; set; }
    }
}
