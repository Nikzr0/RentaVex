namespace RentaVex.Services.Data
{
    using System.Linq;

    using RentaVex.Data.Common.Repositories;
    using RentaVex.Data.Models;
    using RentaVex.Web.ViewModels.Home;

    public class GetCountService : IGetCountService
    {
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IRepository<Image> imagesRepository;
        private readonly IRepository<Product> productsRepository;

        public GetCountService(
            IDeletableEntityRepository<Category> categoriesRepository,
            IRepository<Image> imagesRepository,
            IRepository<Product> productsRepository,
            IRepository<ApplicationUser> userRepository)
        {
            this.categoriesRepository = categoriesRepository;
            this.imagesRepository = imagesRepository;
            this.productsRepository = productsRepository;
            this.userRepository = userRepository;
            this.categoriesRepository = categoriesRepository;
        }

        public IndexViewModel GetCounts()
        {
            // viewModel
            var data = new IndexViewModel
            {
                ProductsCount = this.productsRepository.All().Count(),
                CategoriesCount = this.categoriesRepository.All().Count(),
                ImgesCount = this.imagesRepository.All().Count(),
            };

            return data;
        }
    }
}
