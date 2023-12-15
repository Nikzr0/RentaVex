namespace RentaVex.Web.ViewModels.AllProducts
{
    using RentaVex.Web.ViewModels;
    using System.Collections.Generic;

    public class AllProductsViewModel : PagingViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
