namespace RentaVex.Web.ViewModels.Products
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RatingViewModel
    {
        [Range(0, 6)]
        public int NumberOfStarts { get; set; }

        public int ProductId { get; set; }

        //public int PagesCount => (int)Math.Ceiling((double)this.ProductsCount / this.ItemsPerPage);

        //public bool HasPreviousPage => this.PageNumber >= 2 ? true : false;

        //public bool HasNextPage => this.PageNumber < this.PagesCount;

        //public int PreviousPage => this.PageNumber - 1;

        //public int NextPage => this.PageNumber + 1;

    }
}
