namespace RentaVex.Services.Data
{
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
