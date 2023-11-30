namespace RentaVex.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ActionConstraints;
    using NuGet.Protocol.Core.Types;
    using RentaVex.Data.Models;
    using RentaVex.Services.Data;
    using RentaVex.Web.ViewModels.InputModel;

    public class AddProductsController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productService;
        private readonly IRentOrSaleService rentOrSale;
        private readonly UserManager<ApplicationUser> userManager;

        public AddProductsController(ICategoriesService categoriesService, IProductsService productService,
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
    }
}
