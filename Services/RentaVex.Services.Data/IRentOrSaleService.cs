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

    public interface IRentOrSaleService
    {
        void RentOrSale(CreateProducInputModel input);
    }
}
