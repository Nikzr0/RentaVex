namespace RentaVex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProductAvailability
    {
        public int ProductAvailabilityID { get; set; }

        public int ProductID { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public bool IsAvailable { get; set; }

        public Product Product { get; set; }
    }
}
