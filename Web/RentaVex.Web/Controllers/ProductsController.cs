namespace RentaVex.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ActionConstraints;
    using Microsoft.Identity.Client;
    using NuGet.Protocol.Core.Types;
    using RentaVex.Data.Models;
    using RentaVex.Services.Data;
    using RentaVex.Web.ViewModels.AllProducts;
    using RentaVex.Web.ViewModels.InputModel;

    public class ProductsController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productService;
        private readonly IRentOrSaleService rentOrSale;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsController(ICategoriesService categoriesService, IProductsService productService,
            IRentOrSaleService rentOrSale, UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.productService = productService;
            this.rentOrSale = rentOrSale;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateProducInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetCategories(); // category dropdown

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProducInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetCategories();

                return this.View();
            }

            // We difine usermanagerm, which can provide use info about the user.
            var user = await this.userManager.GetUserAsync(this.User);
            this.rentOrSale.RentOrSale(input);

            await this.productService.CreateAsync(input, user.Id);

            return this.Redirect("/");
        }

        public IActionResult All(int id = 1)
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
