using _2_TodoListWithLocalDb.Domains;
using Microsoft.EntityFrameworkCore;

namespace _2_TodoListWithLocalDb.Datas
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
    }
}
