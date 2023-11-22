namespace RentaVex.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ActionConstraints;
    using RentaVex.Services.Data;
    using RentaVex.Web.ViewModels.InputModel;

    public class ProductsController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productService;

        public ProductsController(ICategoriesService categoriesService, IProductsService productService)
        {
            this.categoriesService = categoriesService;
            this.productService = productService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateProducInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetCategories();

            return this.View(viewModel);
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateProducInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetCategories();

                // input.ConditionItems = this.conditionService.GetCondition(); // The condition service was deleted
                return this.View(input);
            }

            await this.productService.CreateAsync(input);

            return this.Redirect("/");

            // return this.Json(input);
        }
    }
}
