using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI
{
    public class AppDbContext:DbContext
    {
        public DbSet<TodoItem> todoItems { get; set; }  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)            
        {
            optionsBuilder.UseSqlite("Data Source=todo.db");
        }
    }
}
