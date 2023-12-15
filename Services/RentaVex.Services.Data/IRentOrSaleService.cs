namespace RentaVex.Services.Data
{
    using RentaVex.Web.ViewModels.InputModel;

    public interface IRentOrSaleService
    {
        void RentOrSale(CreateProducInputModel input);
    }
}
