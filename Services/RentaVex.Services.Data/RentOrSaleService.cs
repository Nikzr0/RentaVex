namespace RentaVex.Services.Data
{
    using RentaVex.Web.ViewModels.Products;

    public class RentOrSaleService : IRentOrSaleService
    {
        public void RentOrSale(CreateProducViewModel input)
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
