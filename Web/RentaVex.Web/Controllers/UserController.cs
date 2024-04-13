namespace RentaVex.Web.Controllers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RentaVex.Data.Models;
    using RentaVex.Services.Data;
    using RentaVex.Web.ViewModels.AllProducts;
    using RentaVex.Web.ViewModels.User;

    public class UserController : Controller
    {
        private readonly IProductsService productService;

        public UserController(IProductsService productService)
        {
            this.productService = productService;
        }

        public IActionResult MyProducts(int id = 1)
        {
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

        public IActionResult Message()
        {
            return this.View();
        }

        public IActionResult Liked()
        {
            return this.View();
        }

        public IActionResult AddProduct()
        {
            return this.View();
        }
    }
}
