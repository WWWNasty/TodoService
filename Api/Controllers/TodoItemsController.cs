using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLayer.Abstracction;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace TodoApiDTO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemsService _itemsService;
        
        public TodoItemsController(ITodoItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpGet]
        public async Task<IEnumerable<TodoItemDto>> GetTodoItems() =>
            await _itemsService.GetAllAsync();

        [HttpGet("{id:long}")]
        public async Task<TodoItemDto> GetTodoItem(long id) => 
            await _itemsService.GetAsync(id);

        [HttpPut("{dto.id:long}")]
        public async Task<TodoItemDto> UpdateTodoItem(TodoItemDto dto) =>
            await _itemsService.UpdateAsync(dto);

        [HttpPost]
        public async Task<TodoItemDto> CreateTodoItem(TodoItemDto dto) =>
            await _itemsService.AddAsync(dto);
        
        [HttpDelete("{id:long}")]
        public async Task Delete(long id) =>
            await _itemsService.DeleteAsync(id);
    }
}
