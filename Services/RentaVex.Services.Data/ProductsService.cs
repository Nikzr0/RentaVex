namespace RentaVex.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using RentaVex.Data;
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
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly ApplicationDbContext dbContext;

        public ProductsService(IDeletableEntityRepository<Product> productRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            ApplicationDbContext dbContext)
        {
            this.productRepository = productRepository;
            this.userRepository = userRepository;
            this.dbContext = dbContext;
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

        public ApplicationUser GetUserById(string userId)
        {
            return this.userRepository.AllAsNoTracking()
                                      .FirstOrDefault(x => x.Id == userId);
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

            var unavailableDays = Enumerable.Range(0, (end - start).Days + 1)
                                      .Select(offset => start.AddDays(offset))
                                      .ToList();

            var newUnavailableDates = unavailableDays.Where(date => !product.UnavailableDates.Any(ud => ud.Date == date))
                                              .ToList();

            foreach (var date in newUnavailableDates)
            {
                product.UnavailableDates.Add(new UnavailableDate { Date = date });
            }

            await this.productRepository.SaveChangesAsync();
        }

        public async Task LikeProductAsync(int productId, string userId)
        {
            var product = await this.productRepository.All().FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                throw new ArgumentException($"Product with ID {productId} is not found.");
            }

            var user = this.GetUserById(userId);

            if (user == null)
            {
                throw new ArgumentException($"User with ID {userId} is not found.");
            }

            if (product.UserId != userId)
            {
                if (!user.LikedProducts.Any(p => p.Id == productId))
                {
                    //user.LikedProducts.Add(product);
                    this.dbContext.Users.Find(userId).LikedProducts.Add(product);
                    await this.dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new InvalidOperationException($"Product with ID {productId} is already liked by user with ID {userId}.");
                }
            }
        }

        public async Task RateProductById(RatingViewModel model, int numberOfStars)
        {
            var product = this.GetProduct(model.ProductId);
            var rating = new ProductRating { ProductId = model.ProductId, Product = product, NumberOfStars = numberOfStars };

            product.ProductRatings.Add(rating);
            await this.productRepository.SaveChangesAsync();
        }

        public IEnumerable<ProductViewModel> GetLikedProductsForUser(string userId)
        {
            var likedProducts = this.userRepository.All()
                            .Where(u => u.Id == userId)
                            .OrderByDescending(p => p.Id)
                            .SelectMany(u => u.LikedProducts)
                            .Select(x => new ProductViewModel
                            {
                                Id = x.Id,
                                Name = x.Name,
                                Description = x.Description,
                                Price = x.Price,
                                Category = x.Category.Name,
                                Location = x.Location,
                                UserId = x.UserId,
                                ImageUrl = x.Images.FirstOrDefault().ImageUrl,
                            })
                            .ToList();

            return likedProducts;
        }

        public int GetLikedProductsCountForUser(string userId)
        {
            var likedProductsCount = this.userRepository.All()
                .Where(u => u.Id == userId)
                .SelectMany(u => u.LikedProducts)
                .Count();

            return likedProductsCount;
        }
    }
}
