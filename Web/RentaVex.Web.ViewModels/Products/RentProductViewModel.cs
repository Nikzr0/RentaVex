namespace RentaVex.Web.ViewModels.Products
{
    using System;

    using RentaVex.Web.ViewModels.AllProducts;

    public class RentProductViewModel
    {
        public RentProductViewModel()
        {
            this.PickupTime = DateTime.UtcNow.Date.AddDays(1);
            this.ReturnTime = DateTime.UtcNow.Date.AddDays(1);
        }

        public ProductViewModel Product { get; set; }

        public DateTime PickupTime { get; set; }

        public DateTime ReturnTime { get; set; }
    }
}