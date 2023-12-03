namespace RentaVex.Web.ViewModels
{
    using System;

    public class PagingViewModel
    {
        public int ProductsCount { get; set; }

        public int ItemsPerPage { get; set; }

        public int PageNumber { get; set; } // curr page

        public int PagesCount => (int)Math.Ceiling((double)this.ProductsCount / this.ItemsPerPage);

        public bool HasPreviousPage => this.PageNumber >= 2 ? true : false;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int PreviousPage => this.PageNumber - 1;

        public int NextPage => this.PageNumber + 1;
    }
}
