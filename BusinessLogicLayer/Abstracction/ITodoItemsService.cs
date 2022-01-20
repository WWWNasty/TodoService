using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Abstracction
{
    public interface ITodoItemsService
    { 
        Task<IEnumerable<TodoItemDto>> GetAllAsync();
        Task<TodoItemDto> GetAsync(long id); 
        Task<TodoItemDto> AddAsync(TodoItemDto dto); 
        Task DeleteAsync(long id); 
        Task<TodoItemDto> UpdateAsync(TodoItemDto dto);
    }
}