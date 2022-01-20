using System.Threading.Tasks;
using DataAccessLayer.Abstraction.RepositoryInterfaces;

namespace DataAccessLayer.Abstraction
{
    public interface IUnitOfWork
    {
        ITodoItemRepository TodoItemRepository { get; }

        Task SaveChangesAsync();
    }
}