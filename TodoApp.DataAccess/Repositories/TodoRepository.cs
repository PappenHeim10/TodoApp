using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoApp.Core.DTOs;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;

namespace TodoApp.DataAccess.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TodoRepository> _logger;

        public TodoRepository(AppDbContext context, ILogger<TodoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            try
            {
                return await _context.Todos.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all todo items.");
                return Enumerable.Empty<TodoItem>();
            }
        }

        public async Task<CreateTodoResponse> AddAsync(TodoItem item)
        {
            try
            {
                _context.Todos.Add(item);

                await _context.SaveChangesAsync();

                return new CreateTodoResponse
                {
                    Success = true,
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    Created = item.Created,
                    Updated = item.Updated,
                    Status = item.Status,
                    UserId = item.UserId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new todo item.");

                return new CreateTodoResponse
                {
                    Success = false,
                    Id = null,
                    Title = null,
                    Description = null,
                    Created = null,
                    Updated = null,
                    Status = null,
                    UserId = null
                };
            }
        }


        public async Task<bool> DeleteTodo(int id)
        {
            TodoItem? todo = await _context.Todos.FindAsync(id);

            if(todo == null)
            {
                return false;
            }
            else
            {
                _context.Todos.Remove(todo);

                await _context.SaveChangesAsync();

                return true;
            }
        }
    }
}