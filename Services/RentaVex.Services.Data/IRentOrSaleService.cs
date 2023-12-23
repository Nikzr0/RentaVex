namespace RentaVex.Services.Data
{
    using RentaVex.Web.ViewModels.Products;

    public interface IRentOrSaleService
    {
        void RentOrSale(CreateProducViewModel input);
    }
}
