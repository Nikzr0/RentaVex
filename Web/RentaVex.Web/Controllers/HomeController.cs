namespace RentaVex.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentaVex.Services.Data;
    using RentaVex.Web.ViewModels;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly IGetCountService countService;

        public HomeController(IGetCountService countService)
        {
            this.countService = countService;
        }

        public IActionResult Index()
        {
            var viewModel = this.countService.GetCounts();
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult ContactUs()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
