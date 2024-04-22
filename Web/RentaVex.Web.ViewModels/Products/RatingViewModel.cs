namespace RentaVex.Web.ViewModels.Products
{
    using System;

    public class RatingViewModel
    {
        public int NumberOfStarts { get; set; }

        public double AverageRating { get; set; }


        //public int PagesCount => (int)Math.Ceiling((double)this.ProductsCount / this.ItemsPerPage);

        //public bool HasPreviousPage => this.PageNumber >= 2 ? true : false;

        //public bool HasNextPage => this.PageNumber < this.PagesCount;

        //public int PreviousPage => this.PageNumber - 1;

        //public int NextPage => this.PageNumber + 1;

    }
}
