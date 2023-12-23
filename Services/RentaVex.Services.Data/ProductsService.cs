﻿namespace RentaVex.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using RentaVex.Data.Common.Repositories;
    using RentaVex.Data.Models;
    using RentaVex.Services.Mapping;
    using RentaVex.Web.ViewModels.AllProducts;
    using RentaVex.Web.ViewModels.Products;

    public class ProductsService : IProductsService
    {
        private readonly string[] allowedExtentions = new[] { "jpg", "png", "gif", "jpeg" };

        private readonly IDeletableEntityRepository<Product> productRepository;

        public ProductsService(IDeletableEntityRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task CreateAsync(CreateProducViewModel inputInfo, string userId, string imagePath)
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

            // image
            Directory.CreateDirectory($"{imagePath}/products/");
            foreach (var image in inputInfo.Images)
            {
                var extention = Path.GetExtension(image.FileName).TrimStart('.').ToLower();
                if (!this.allowedExtentions.Any(x => extention.EndsWith(x)))
                {
                    throw new Exception($"Invalid image type extention: {extention}");
                }

                var dbImage = new Image
                {
                    AddedByUser = userId,
                    Extention = extention,
                };

                product.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/products/{dbImage.Id}.{extention}";

                using (Stream fileStream = new FileStream(physicalPath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            }

            await this.productRepository.AddAsync(product);
            await this.productRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            var products = this.productRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();

            return products;
        }

        public int GetCount()
        {
            return this.productRepository.All().Count();
        }

        public ProductViewModel GetProductById(int id)
        {
            var productEntity = this.productRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<ProductViewModel>()
                .FirstOrDefault();

            return productEntity;
        }

        public void SetProductUnavailableDates(int productId, DateTime start, DateTime end)
        {
            var product = this.GetProductById(productId);

            public void SetUnavailableDates(DateTime start, DateTime end)
            {
                // Assuming Availabilities is initialized in the constructor
                Availabilities ??= new HashSet<ProductAvailability>();

                // Generate a list of dates between start and end (inclusive)
                var unavailableDates = Enumerable.Range(0, (end - start).Days + 1)
                                                 .Select(offset => start.AddDays(offset))
                                                 .ToList();

                // Remove existing availabilities within the selected range
                Availabilities.RemoveWhere(avail => unavailableDates.Contains(avail.AvailableDate));

                // Add new unavailability entries
                foreach (var date in unavailableDates)
                {
                    Availabilities.Add(new ProductAvailability { AvailableDate = date });
                }
            }
        }
    }
}
