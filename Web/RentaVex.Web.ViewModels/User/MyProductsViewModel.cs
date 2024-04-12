namespace RentaVex.Web.ViewModels.User
{
    using System.Collections.Generic;

    using RentaVex.Web.ViewModels.AllProducts;

    public class MyProductsViewModel : PagingViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }

    }
}
