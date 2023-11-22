namespace RentaVex.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ActionConstraints;
    using NuGet.Protocol.Core.Types;
    using RentaVex.Services.Data;
    using RentaVex.Web.ViewModels.InputModel;

    public class ProductsController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productService;
        private readonly IRentOrSaleService rentOrSale;

        public ProductsController(ICategoriesService categoriesService, IProductsService productService,
            IRentOrSaleService rentOrSale)
        {
            this.categoriesService = categoriesService;
            this.productService = productService;
            this.rentOrSale = rentOrSale;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateProducInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetCategories(); // category dropdown

            return this.View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateProducInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetCategories();

                return this.View();
            }

            this.rentOrSale.RentOrSale(input);

            await this.productService.CreateAsync(input);

            return this.Redirect("/");
        }
    }
}
