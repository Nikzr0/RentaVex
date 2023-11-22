namespace RentaVex.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RentaVex.Data.Common.Repositories;
    using RentaVex.Data.Models;
    using RentaVex.Web.ViewModels.InputModel;

    public class RentOrSaleService : IRentOrSaleService
    {
        public void RentOrSale(CreateProducInputModel input)
        {
            if (input.IsForRent)
            {
                input.IsForRent = true;
                input.IsForSale = false;
            }
            else
            {
                input.IsForRent = false;
                input.IsForSale = true;
            }
        }
    }
}
