namespace RentaVex.Data.Models
{
    using RentaVex.Data.Common.Models;

    public class ProductRating : BaseDeletableModel<int>
    {
        public int RatingCount { get; set; }

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
