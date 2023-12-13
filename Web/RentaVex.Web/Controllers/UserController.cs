namespace RentaVex.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class UserController : Controller
    {
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

        public IActionResult MyProducts()
        {
            return this.View();
        }
    }
}
