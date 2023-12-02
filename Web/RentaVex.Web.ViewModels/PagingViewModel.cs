using System;

namespace RentaVex.Web.ViewModels
{
    public class PagingViewModel
    {
        public int ProductsCount { get; set; }

        public int ItemsPerPage { get; set; }

        public int PageNumber { get; set; } // curr page

        public int PagesCount => (int)Math.Ceiling((double)ProductsCount / ItemsPerPage);

        public bool HasPreviousPage => PageNumber >= 2 ? true : false;

        public bool HasNextPage => PageNumber < PagesCount;

        public int PreviousPage => PageNumber - 1;

        public int NextPage => PageNumber + 1;
    }
}