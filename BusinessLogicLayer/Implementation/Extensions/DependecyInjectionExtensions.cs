using BusinessLogicLayer.Abstracction;
using BusinessLogicLayer.Implementation.Services;
using BusinessLogicLayer.Models.MapperProfiles;
using DataAccessLayer.Implementation.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer.Implementation.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddBlDependencies(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddDalDependencies(configuration);

            collection.AddScoped<ITodoItemsService, TodoItemsService>();
            collection.AddAutoMapper(options => options.AddProfile<MappingProfile>());

            return collection;
        }
    }
}