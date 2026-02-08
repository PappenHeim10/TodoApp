using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Entities;

namespace TodoApp.DataAccess
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) :base(options)
        {

        }

        public DbSet<TodoItem> Todos { get; set; }
    }
}