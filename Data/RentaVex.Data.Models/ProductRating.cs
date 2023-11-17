namespace RentaVex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RentaVex.Data.Common.Models;

    public class ProductRating : BaseDeletableModel<int>
    {
        public int RatingCount { get; set; } // TODO : Add to it

        public double Sum { get; set; }

        public double AverageRating
        {
            get
            {
                if (this.RatingCount == 0)
                {
                    return 0;
                }

                return (double)this.Sum / this.RatingCount;
            }
        }
    }
}
