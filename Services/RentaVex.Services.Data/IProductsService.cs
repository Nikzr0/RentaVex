namespace RentaVex.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RentaVex.Data.Models;
    using RentaVex.Web.ViewModels.AllProducts;
    using RentaVex.Web.ViewModels.Products;
    using RentaVex.Web.ViewModels.User;

    public interface IProductsService
    {
        Task CreateAsync(CreateProducViewModel createProducts, string userId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        int GetCount();

        ProductViewModel GetProductById(int id);

        Product GetProduct(int id);

        Task SetProductUnavailableDates(Product product, DateTime start, DateTime end);

        Task EditProductAsync(int id, EditProductViewModel input);

        Task RemoveProductAsync(int id);

        Task LikeProductAsync(int productId, string userId);

        Task UnLikeProductAsync(int productId, string userId);

        Task RateProductById(int productId, int ratingValue);

        IEnumerable<ProductViewModel> GetLikedProductsForUser(string userId);

        int GetLikedProductsCountForUser(string userId);

        double GetAverageRating(int productId);
    }
}
