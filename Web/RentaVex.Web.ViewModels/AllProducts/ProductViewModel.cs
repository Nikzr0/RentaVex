namespace RentaVex.Web.ViewModels.AllProducts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;
    using RentaVex.Data.Models;
    using RentaVex.Services.Mapping;

    public class ProductViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ProductViewModel>()
                .ForMember(x => x.ImageUrl, opt =>

                opt.MapFrom(x => x.Images.FirstOrDefault().ImageUrl != null ?
                x.Images.FirstOrDefault().ImageUrl :
                "images/recipes" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extention));
        }
    }
}
