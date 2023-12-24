namespace RentaVex.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RentaVex.Web.ViewModels.AllProducts;
    using RentaVex.Web.ViewModels.Products;

    public interface IProductsService
    {
        Task CreateAsync(CreateProducViewModel createProducts, string userId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        int GetCount();

        ProductViewModel GetProductById(int id);

        Task SetProductUnavailableDates(int productId, DateTime start, DateTime end);
    }
}
