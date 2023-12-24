namespace RentaVex.Web.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RentProductViewModel
    {
        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public DateTime PickupTime { get; set; }

        public DateTime ReturnTime { get; set; }

        public int ProductId { get; set; }
    }
}