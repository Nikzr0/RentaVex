//namespace RentaVex.Web.ViewModels.AllProducts
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;

//    using AutoMapper;
//    using RentaVex.Data.Models;
//    using RentaVex.Services.Mapping;

//    public class UserViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
//    {
//        public int Id { get; set; }

//        public string ImageUrl { get; set; }

//        public string Description { get; set; }

//        public string Name { get; set; }

//        public string Category { get; set; }

//        public decimal Price { get; set; }

//        public string Location { get; set; }

//        public DateTime CreatedOn { get; set; }

//        public string UserId { get; set; }

//        public ICollection<Product> LikedProducts { get; set; }



//        public void CreateMappings(IProfileExpression configuration)
//        {
//            configuration.CreateMap<Product, ProductViewModel>()
//                .ForMember(x => x.ImageUrl, opt =>
//                opt.MapFrom(x => x.Images.FirstOrDefault().ImageUrl != null ?
//                x.Images.FirstOrDefault().ImageUrl :
//                "/images/products/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extention))
//                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn));
//        }
//    }
//}
