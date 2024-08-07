﻿namespace RentaVex.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RentaVex.Common;
    using RentaVex.Data.Models;
    using RentaVex.Services.Data;
    using RentaVex.Web.ViewModels.AllProducts;
    using RentaVex.Web.ViewModels.Products;
    using RentaVex.Web.ViewModels.User;

    public class UserController : Controller
    {
        private readonly IProductsService productService;
        private readonly ICategoriesService categoriesService;

        public UserController(IProductsService productService, ICategoriesService categoriesService)
        {
            this.productService = productService;
            this.categoriesService = categoriesService;
        }

        public IActionResult MyProducts(int id = 1)
        {
            string pageName = GlobalService.GetPageName(this.HttpContext);
            const int itemsPerPage = 24;

            if (id < 1)
            {
                return this.NotFound();
            }

            var viewModel = new MyProductsViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                Products = this.productService.GetAll<ProductViewModel>(id, itemsPerPage),
                ProductsCount = this.productService.GetCount(),
            };

            return this.View(viewModel);
        }

        public IActionResult MyRentings()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult EditProduct(int id)
        {
            var product = this.productService.GetProduct(id);

            var categories = this.categoriesService.GetCategories();

            var viewModel = new EditProductViewModel();
            viewModel.Product = product;
            viewModel.Categories = categories;
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditProduct(int id, EditProductViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(id);
            }

            try
            {
                await this.productService.EditProductAsync(id, input);
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "An error occurred while editing the product.");
                return this.View(input);
            }

            return this.RedirectToAction("MyProducts", "User");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            await this.productService.RemoveProductAsync(id);
            return this.RedirectToAction("MyProducts", "User");
        }

        public IActionResult Message()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Liked(int pageNumver = 1)
        {
            const int itemsPerPage = 24;

            if (pageNumver < 1)
            {
                return this.NotFound();
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var likedProducts = this.productService.GetLikedProductsForUser(userId);

            var viewModel = new AllProductsViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = pageNumver,
                Products = likedProducts,
                ProductsCount = this.productService.GetLikedProductsCountForUser(userId),
            };

            return this.View(viewModel);
        }
    }
}
