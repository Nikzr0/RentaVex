namespace RentaVex.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RentaVex.Data.Models;
    using RentaVex.Web.ViewModels.AllProducts;
    using RentaVex.Web.ViewModels.Products;

    public interface IProductsService
    {
        Task CreateAsync(CreateProducViewModel createProducts, string userId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        int GetCount();

        ProductViewModel GetProductById(int id);

        Product GetProduct(int id);

        Task SetProductUnavailableDates(Product product, DateTime start, DateTime end);
    }
}
