namespace RentaVex.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RentaVex.Web.ViewModels.AllProducts;
    using RentaVex.Web.ViewModels.InputModel;

    public interface IProductsService
    {
        Task CreateAsync(CreateProducInputModel createProducts, string userId);

        IEnumerable<ProductViewModel> GetAll(int page, int itemsPerPage);
    }
}
