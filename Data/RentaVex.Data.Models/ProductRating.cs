using System.ComponentModel.DataAnnotations;

using RentaVex.Data.Models;

public class ProductRating
{
    public int Id { get; set; }

    [Range(0, int.MaxValue)]
    public int NumberOfStars { get; set; }

    public double AverageRating { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }
}