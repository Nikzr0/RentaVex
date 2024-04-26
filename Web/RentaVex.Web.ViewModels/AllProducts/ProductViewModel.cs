namespace RentaVex.Web.ViewModels.AllProducts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using RentaVex.Data.Models;
    using RentaVex.Services.Mapping;
    using RentaVex.Web.ViewModels.Products;

    public class ProductViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public string Location { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }

        //[Range(0, 6)]
        //public int NumberOfStars { get; set; }

        //public int ProductId { get; set; }

        //public double AverageRating { get; set; }

        public ICollection<ProductRating> ProductRatings { get; set; }

        public double AverageRating { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ProductViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                opt.MapFrom(x => x.Images.FirstOrDefault().ImageUrl != null ?
                x.Images.FirstOrDefault().ImageUrl :
                "/images/products/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extention))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src =>
                    src.ProductRatings.Any() ? src.ProductRatings.Average(r => r.NumberOfStars) : 0));
        }
    }
}
