using DataAccessLayer.Abstraction.RepositoryInterfaces.Base;
using DataAccessLayer.Models;

namespace DataAccessLayer.Abstraction.RepositoryInterfaces
{
    public interface ITodoItemRepository : IBaseRepository<TodoItem, long>
    {
    }
}