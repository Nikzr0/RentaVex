namespace RentaVex.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentaVex.Common;
    using RentaVex.Services.Data;
    using RentaVex.Web.ViewModels.AllProducts;

    public class BaseShopPagesController : Controller
    {
        protected readonly IProductsService productService;

        public BaseShopPagesController(IProductsService productService)
        {
            this.productService = productService;
        }

        protected IActionResult GetProductsView(int id)
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
