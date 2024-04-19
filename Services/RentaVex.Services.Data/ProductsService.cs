﻿using Microsoft.EntityFrameworkCore;
using RentaVex.Data.Models;
using System;

namespace RentaVex.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using RentaVex.Data.Common.Repositories;
    using RentaVex.Data.Models;
    using RentaVex.Services.Mapping;
    using RentaVex.Web.ViewModels.AllProducts;
    using RentaVex.Web.ViewModels.Products;
    using RentaVex.Web.ViewModels.User;

    public class ProductsService : IProductsService
    {
        private readonly string[] allowedExtentions = new[] { "jpg", "png", "gif", "jpeg" };
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IDeletableEntityRepository<User> userRepository;

        public ProductsService(IDeletableEntityRepository<Product> productRepository, IDeletableEntityRepository<User> userRepository)
        {
            this.productRepository = productRepository;
            this.userRepository = userRepository;
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

            product.CourierDelivery = inputInfo.CourierDelivery;

            product.CategoryId = inputInfo.CategoryId;

            product.Condition = inputInfo.Condition;

            product.IsWarned = inputInfo.IsWarned;
            product.WarningMessage = inputInfo.WarningMessage;

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

            var user = this.GetUserById(userId);

            if (user != null)
            {
                user.MyProducts.Add(product);
            }
            else
            {
                // Handle case where user is not found
                // For example, throw an exception or log an error
            }

            await this.productRepository.AddAsync(product);
            await this.productRepository.SaveChangesAsync();
        }

        public async Task EditProductAsync(int productId, EditProductViewModel inputInfo)
        {
            var productToEdit = await this.productRepository.All().FirstOrDefaultAsync(p => p.Id == productId);

            if (productToEdit == null)
            {
                throw new ArgumentException($"Product with ID {productId} not found.");
            }

            if (true)
            {

            }

            productToEdit.Name = inputInfo.Product.Name;
            productToEdit.Description = inputInfo.Product.Description;
            productToEdit.IsForRent = inputInfo.Product.IsForRent;
            productToEdit.IsForSale = inputInfo.Product.IsForSale;
            productToEdit.Price = inputInfo.Product.Price;
            productToEdit.Location = inputInfo.Product.Location;
            productToEdit.Contact = inputInfo.Product.Contact;
            productToEdit.CategoryId = inputInfo.Product.CategoryId;
            productToEdit.CourierDelivery = inputInfo.Product.CourierDelivery;
            productToEdit.Condition = inputInfo.Product.Condition;
            productToEdit.IsWarned = inputInfo.Product.IsWarned;
            productToEdit.WarningMessage = inputInfo.Product.WarningMessage;

            await this.productRepository.SaveChangesAsync();
        }

        public async Task RemoveProductAsync(int id)
        {
            var productToRemove = await this.productRepository.All().FirstOrDefaultAsync(p => p.Id == id);

            if (productToRemove != null)
            {
                this.productRepository.Delete(productToRemove);
                await this.productRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Product not found!");
            }
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

        public User GetUserById(string userId)
        {
            return this.userRepository.AllAsNoTracking()
                                      .FirstOrDefault(x => x.Id.ToString() == userId);
        }

        public ProductViewModel GetProductById(int id)
        {
            var productEntity = this.productRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<ProductViewModel>()
                .FirstOrDefault();

            return productEntity;
        }

        public Product GetProduct(int productId)
        {
            return this.productRepository.AllAsNoTracking()
                                         .FirstOrDefault(x => x.Id == productId);
        }

        public async Task SetProductUnavailableDates(Product product, DateTime start, DateTime end)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "The product is null.");
            }

            var unavailableDates = Enumerable.Range(0, (end - start).Days + 1)
                                       .Select(offset => start.AddDays(offset))
                                       .ToList();

            product.UnavailableDates.Clear();

            foreach (var date in unavailableDates)
            {
                product.UnavailableDates.Add(new UnavailableDate { Date = date });
            }

            await this.productRepository.SaveChangesAsync();
        }

        //public async Task<Product> GetProductAsync(int productId)
        //{
        //    return await Task.Run(() => GetProduct(productId));
        //}

        //TODO
        //public async Task LikeProductAsync(int productId, string userId)
        //{
        //    // Get the product by its ID asynchronously
        //    var product = await GetProductAsync(productId);

        //    if (product == null)
        //    {
        //        throw new ArgumentException("Product not found");
        //    }

        //    // Add the user's ID to the liked products
        //    product.LikedByUsers.Add(userId);

        //    await productRepository.SaveChangesAsync();
        //}
    }
}
