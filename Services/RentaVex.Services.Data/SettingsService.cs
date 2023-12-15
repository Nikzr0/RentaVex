namespace RentaVex.Services.Data
{
    using RentaVex.Data.Common.Repositories;
    using RentaVex.Data.Models;
    using RentaVex.Services.Mapping;
    using System.Collections.Generic;
    using System.Linq;

    public class SettingsService : ISettingsService
    {
        private readonly IDeletableEntityRepository<Setting> settingsRepository;

        public SettingsService(IDeletableEntityRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public int GetCount()
        {
            return this.settingsRepository.AllAsNoTracking().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.settingsRepository.All().To<T>().ToList();
        }
    }
}
