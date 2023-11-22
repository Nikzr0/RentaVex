namespace RentaVex.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RentaVex.Data.Common.Repositories;
    using RentaVex.Data.Models;
    using RentaVex.Web.ViewModels.InputModel;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;

        public ProductsService(IDeletableEntityRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task CreateAsync(CreateProducInputModel inputInfo)
        {
            var product = new Product();

            product.Name = inputInfo.Name;
            product.Description = inputInfo.Description;
            product.IsForRent = inputInfo.IsForRent;
            product.IsForSale = inputInfo.IsForSale;
            product.Price = inputInfo.Price;
            product.Location = inputInfo.Location;
            product.Contact = inputInfo.Contact;
            product.CategoryID = inputInfo.CategoryID;

            // They should not be implemented here
            // They will be moved in the future
            product.PickupTime = inputInfo.PickupTime;
            product.ReturnTime = inputInfo.ReturnTime;

            product.CourierDelivery = inputInfo.CourierDelivery;

            // product.Condition = inputInfo.Condition; -->> TODO
            product.CategoryID = inputInfo.CategoryID;

            await this.productRepository.AddAsync(product);
            await this.productRepository.SaveChangesAsync();
        }
    }
}
