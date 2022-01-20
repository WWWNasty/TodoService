using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Abstraction.RepositoryInterfaces.Base;
using DataAccessLayer.DataAccess;
using DataAccessLayer.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Implementation.Repositories.Base
{
    public abstract class BaseRepository<T> : IBaseRepository<T, long> where T : Entity<long>
    {
        private readonly TodoContext _dbContext;

        protected BaseRepository(TodoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual Task<T> GetAsync(long id) =>
            GetData().FirstOrDefaultAsync(entity => entity.Id == id);

        public virtual async Task<ICollection<T>> GetAllAsync() => 
            await GetData().ToListAsync();

        public virtual void Delete(T entity) => 
            GetData().Remove(entity);
        
        public virtual long Create(T entity) =>
            _dbContext.Set<T>().Add(entity).Entity.Id;

        public virtual long Update(T entity) =>
            _dbContext.Set<T>().Update(entity).Entity.Id;

        private DbSet<T> GetData() => 
            _dbContext.Set<T>();
        }
}