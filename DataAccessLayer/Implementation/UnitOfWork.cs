using System.Threading.Tasks;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Abstraction.RepositoryInterfaces;

namespace DataAccessLayer.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoContext _dbContext;
        public ITodoItemRepository TodoItemRepository { get; }

        public UnitOfWork(TodoContext dbContext, ITodoItemRepository todoItemRepository)
        {
            _dbContext = dbContext;
            TodoItemRepository = todoItemRepository;
        }
        
        public Task SaveChangesAsync() =>
            _dbContext.SaveChangesAsync();
    }
}