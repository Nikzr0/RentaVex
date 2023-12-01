namespace RentaVex.Web.ViewModels.AllProducts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AllProductsViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }

        public int PageNumber { get; set; }
    }
}
