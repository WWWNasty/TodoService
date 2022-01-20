using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Models.Base;

namespace DataAccessLayer.Abstraction.RepositoryInterfaces.Base
{
    public interface IBaseRepository<TEntity, in TId> where TEntity : Entity<TId>
    {
        Task<TEntity> GetAsync(TId id);
        Task<ICollection<TEntity>> GetAllAsync();
        void Delete(TEntity entity);

        long Create(TEntity entity);
        long Update(TEntity entity);
    }
}