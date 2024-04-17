namespace RentaVex.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using RentaVex.Data.Models;
    using RentaVex.Services.Data;
    using RentaVex.Web.ViewModels.AllProducts;
    using RentaVex.Web.ViewModels.Products;
    using RentaVex.Web.ViewModels.User;

    public class ProductsController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productService;
        private readonly IRentOrSaleService rentOrSale;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public ProductsController(ICategoriesService categoriesService, IProductsService productService,
            IRentOrSaleService rentOrSale, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            this.categoriesService = categoriesService;
            this.productService = productService;
            this.rentOrSale = rentOrSale;
            this.userManager = userManager;
            this.environment = environment;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateProducViewModel();
            viewModel.CategoriesItems = this.categoriesService.GetCategories();

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProducViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetCategories();
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            this.rentOrSale.RentOrSale(input);

            try
            {
                //var product = new Product();

                await this.productService
                    .CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");

                //product.Name = input.Name;
                //product.Description = input.Description;
                //product.IsForRent = input.IsForRent;
                //product.IsForSale = input.IsForSale;
                //product.Price = input.Price;
                //product.Location = input.Location;
                //product.Contact = input.Contact;
                //product.CategoryId = input.CategoryId;
                //product.UserId = user.Id;

                //product.CourierDelivery = input.CourierDelivery;

                //product.CategoryId = input.CategoryId;

                //product.Condition = input.Condition;

                //product.IsWarned = input.IsWarned;
                //product.WarningMessage = input.WarningMessage;


                //user.MyProducts.Add(product);
                //await this.userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.CategoriesItems = this.categoriesService.GetCategories();
                return this.View(input);
            }

            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Buy(int id = 1)
        {
            const int itemsPerPage = 24;

            if (id < 1)
            {
                return this.NotFound();
            }

            var viewModel = new AllProductsViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                Products = this.productService.GetAll<ProductViewModel>(id, itemsPerPage),
                ProductsCount = this.productService.GetCount(),
            };

            return this.View(viewModel);
        }

        //public IActionResult Rent(int id = 1)
        //{
        //    const int itemsPerPage = 24;

        //    if (id < 1)
        //    {
        //        return this.NotFound();
        //    }

        //    var viewModel = new AllProductsViewModel
        //    {
        //        ItemsPerPage = itemsPerPage,
        //        PageNumber = id,
        //        MyProducts = this.productService.GetAll<ProductViewModel>(id, itemsPerPage),
        //        ProductsCount = this.productService.GetCount(),
        //    };

        //    return this.View(viewModel);
        //}

        public IActionResult ProductPage(int id)
        {
            var product = new ProductPageViewModel
            {
                Product = this.productService.GetProductById(id),
            };

            return this.View(product);
        }

        [HttpGet]
        public IActionResult RentProduct(int id)
        {
            var product = new RentProductViewModel
            {
                Product = this.productService.GetProductById(id),
            };

            return this.View(product);
        }

        //[HttpPost]
        //public IActionResult RentProduct(RentProductViewModel model)
        //{
        //    if (this.ModelState.IsValid)
        //    {
        //        return this.Redirect("/");
        //    }

        //    return this.View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> RentProduct(RentProductViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var product = this.productService.GetProduct(model.Product.Id);

                if (product == null)
                {
                    return this.NotFound();
                }

                await this.productService.SetProductUnavailableDates(product, model.PickupTime, model.ReturnTime);

                return this.RedirectToAction("Index", "Home");
            }

            return this.View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> LikeProduct(int productId)
        //{
        //    // Get the current user ID (You may need to modify this based on your authentication mechanism)
        //    var userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    // Call the service method to like the product
        //    await this.productService.LikeProductAsync(productId, userId);

        //    return this.Ok();
        //}


        // Show Dropdown Pages

        public IActionResult Free(int id = 1)
        {
            const int itemsPerPage = 24;

            if (id < 1)
            {
                return this.NotFound();
            }

            var viewModel = new AllProductsViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                Products = this.productService.GetAll<ProductViewModel>(id, itemsPerPage),
                ProductsCount = this.productService.GetCount(),
            };

            return this.View(viewModel);
        }
    }
}
