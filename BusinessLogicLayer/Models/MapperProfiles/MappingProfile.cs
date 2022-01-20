using AutoMapper;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Models.MapperProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoItem, TodoItemDto>()
                .ReverseMap()
                .ForMember(destination => destination.Id, opt => opt.Ignore());
        }
    }
}