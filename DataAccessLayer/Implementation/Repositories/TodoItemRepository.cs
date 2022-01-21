using DataAccessLayer.Abstraction.RepositoryInterfaces;
using DataAccessLayer.Implementation.Repositories.Base;
using DataAccessLayer.Models;

namespace DataAccessLayer.Implementation.Repositories
{
    public class TodoItemRepository : BaseRepository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(TodoContext dbContext) : base(dbContext)
        {
        }
    }
}