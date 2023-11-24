namespace RentaVex.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentaVex.Services.Data;

    // using RentaVex.Web.ViewModels.Administration.Dashboard;
    using RentaVex.Web.ViewModels.Home;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public IActionResult Index()
        {
            // var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(/*viewModel*/);
        }
    }
}
