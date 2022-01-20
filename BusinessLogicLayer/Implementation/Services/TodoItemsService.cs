using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Abstracction;
using BusinessLogicLayer.Implementation.Exceptions;
using BusinessLogicLayer.Implementation.Services.Base;
using BusinessLogicLayer.Models;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Implementation.Services
{
    public class TodoItemsService: BaseService, ITodoItemsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TodoItemsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoItemDto>> GetAllAsync()
        {
            var items = await _unitOfWork.TodoItemRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<TodoItemDto>>(items);
        }

        public async Task<TodoItemDto> GetAsync(long id)
        {
            var item = await _unitOfWork.TodoItemRepository.GetAsync(id);

            ThrowIfNotFound(item);

            return _mapper.Map<TodoItemDto>(item);
        }

        public async Task<TodoItemDto> AddAsync(TodoItemDto dto)
        {
            Validate(dto);

            var item = _mapper.Map<TodoItem>(dto);

            _unitOfWork.TodoItemRepository.Create(item);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TodoItemDto>(item);
        }

        public async Task DeleteAsync(long id)
        {
            var item = await _unitOfWork.TodoItemRepository.GetAsync(id);

            ThrowIfNotFound(item);

            _unitOfWork.TodoItemRepository.Delete(item);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TodoItemDto> UpdateAsync(TodoItemDto dto)
        {
            Validate(dto);
            
            var item = await _unitOfWork.TodoItemRepository.GetAsync(dto.Id);

            ThrowIfNotFound(item);
            
            _mapper.Map(dto, item);
            
            _unitOfWork.TodoItemRepository.Update(item);

            await _unitOfWork.SaveChangesAsync();
            
            return dto;
        }
        
        private static void ThrowIfNotFound(TodoItem item)
        {
            if (item == null)
            {
                throw new TodoItemNotFoundException("Item is not found!");
            }
        }
    }
}