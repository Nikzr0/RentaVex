namespace RentaVex.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RentaVex.Web.ViewModels.Home;

   public interface IGetCountService
    {
        IndexViewModel GetCounts();
    }
}
