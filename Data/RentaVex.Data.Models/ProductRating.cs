using System.ComponentModel.DataAnnotations;
using RentaVex.Data.Common.Models;
using RentaVex.Data.Common.Repositories;
using RentaVex.Data.Models;

public class ProductRating : BaseDeletableModel<int>
{
    public int NumberOfStars { get; set; }

    //public double AverageRating { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }
}