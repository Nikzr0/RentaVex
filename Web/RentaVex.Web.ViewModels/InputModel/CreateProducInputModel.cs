namespace RentaVex.Web.ViewModels.InputModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RentaVex.Data.Models;

    public class CreateProducInputModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Location { get; set; }

        public string Contact { get; set; }

        public ConditionType Condition { get; set; }

        public int CategoryID { get; set; }

        public Category Category { get; set; }
        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }
    }
}
