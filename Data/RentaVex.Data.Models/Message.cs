namespace RentaVex.Data.Models
{
    using System;
    using RentaVex.Data.Common.Models;

    public class Message : BaseDeletableModel<int>
    {
        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string Content { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
