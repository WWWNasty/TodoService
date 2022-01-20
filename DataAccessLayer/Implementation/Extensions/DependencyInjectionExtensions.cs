using DataAccessLayer.Abstraction;
using DataAccessLayer.Abstraction.RepositoryInterfaces;
using DataAccessLayer.DataAccess;
using DataAccessLayer.Implementation.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.Implementation.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDalDependencies(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection
                .AddEntityFrameworkSqlServer()
                .AddDbContext<TodoContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ITodoItemRepository, TodoItemRepository>();

            return serviceCollection;
        }
    }
}