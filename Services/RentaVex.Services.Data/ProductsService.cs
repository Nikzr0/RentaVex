namespace RentaVex.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RentaVex.Data.Common.Repositories;
    using RentaVex.Data.Models;
    using RentaVex.Services.Mapping;
    using RentaVex.Web.ViewModels.AllProducts;
    using RentaVex.Web.ViewModels.InputModel;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;

        public ProductsService(IDeletableEntityRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task CreateAsync(CreateProducInputModel inputInfo, string userId)
        {
            var product = new Product();

            product.Name = inputInfo.Name;
            product.Description = inputInfo.Description;
            product.IsForRent = inputInfo.IsForRent;
            product.IsForSale = inputInfo.IsForSale;
            product.Price = inputInfo.Price;
            product.Location = inputInfo.Location;
            product.Contact = inputInfo.Contact;
            product.CategoryId = inputInfo.CategoryId;
            product.UserId = userId;

            product.PickupTime = inputInfo.PickupTime;
            product.ReturnTime = inputInfo.ReturnTime;

            product.CourierDelivery = inputInfo.CourierDelivery;

            product.CategoryId = inputInfo.CategoryId;

            product.Condition = inputInfo.Condition;

            product.IsWarned = inputInfo.IsWarned;
            product.WarningMessage = inputInfo.WarningMessage;

            await this.productRepository.AddAsync(product);
            await this.productRepository.SaveChangesAsync();
        }

        public IEnumerable<ProductViewModel> GetAll(int page, int itemsPerPage)
        {
            var products = this.productRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<ProductViewModel>() // To() -->> For the extra logic for the image url.
                .ToList();

            return products;
        }
    }
}
