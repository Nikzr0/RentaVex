namespace RentaVex.Services.Data
{
    using RentaVex.Web.ViewModels.AllProducts;
    using RentaVex.Web.ViewModels.Products;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductsService
    {
        Task CreateAsync(CreateProducViewModel createProducts, string userId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        int GetCount();

        ProductViewModel GetProductById(int id);
    }
}
