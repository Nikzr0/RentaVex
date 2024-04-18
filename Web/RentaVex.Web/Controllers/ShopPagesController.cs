namespace RentaVex.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentaVex.Services.Data;

    public class ShopPagesController : BaseShopPagesController
    {
        public ShopPagesController(IProductsService productsService) : base(productsService)
        {
        }

        public IActionResult NewAdded(int id = 1)
        {
            return this.GetProductsView(id);
        }

        public IActionResult Promoted(int id = 1)
        {
            return this.GetProductsView(id);
        }

        public IActionResult PopularItems(int id = 1)
        {
            return this.GetProductsView(id);
        }

        public IActionResult BestDeals(int id = 1)
        {
            return this.GetProductsView(id);
        }

        public IActionResult CloseBy(int id = 1)
        {
            return this.GetProductsView(id);
        }

        public IActionResult Services(int id = 1)
        {
            return this.GetProductsView(id);
        }

        public IActionResult Free(int id = 1)
        {
            return this.GetProductsView(id);
        }
    }
}
