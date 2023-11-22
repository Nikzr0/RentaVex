namespace RentaVex.Web.ViewModels.InputModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RentaVex.Data.Models;

    public class CreateProducInputModel
    {
        public CreateProducInputModel()
        {
            this.PickupTime = DateTime.UtcNow.Date.AddDays(1);

            this.ReturnTime = DateTime.UtcNow.Date.AddDays(1);
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(1800)]
        public string Description { get; set; }

        public bool IsForRent { get; set; }

        public bool IsForSale { get; set; }

        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        public string Location { get; set; }

        public string Contact { get; set; }

        public int CategoryID { get; set; }

        // Not for here | I need a availability calender instead | Those props should be whe user wants to rent something
        public DateTime PickupTime { get; set; }

        // Not for here -->> I'll change it in the future
        public DateTime ReturnTime { get; set; }

        public Category Category { get; set; }

        public bool CourierDelivery { get; set; }

        public ConditionType Condition { get; set; }

        // Somethign with the product rating, but it is no early for that.
        public bool IsWarned { get; set; }

        public string WarningMessage { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }
    }
}
