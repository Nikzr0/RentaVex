namespace RentaVex.Web.ViewModels.AllProducts
{
    using System.Collections.Generic;

    using RentaVex.Web.ViewModels;

    public class AllProductsViewModel : PagingViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
