using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataAccess
{
    public class TodoContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }
    }
}