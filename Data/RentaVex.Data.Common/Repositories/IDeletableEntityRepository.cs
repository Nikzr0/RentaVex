namespace RentaVex.Data.Common.Repositories
{
    using System;
    using System.Linq;

    using RentaVex.Data.Common.Models;

    public interface IDeletableEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllAsNoTrackingWithDeleted();

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);

       // System.Threading.Tasks.Task<Data.Models.User> FirstOrDefaultAsync(Func<object, bool> value);
    }
}
