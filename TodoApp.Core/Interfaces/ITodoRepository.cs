using TodoApp.Core.DTOs;
using TodoApp.Core.Entities;

namespace TodoApp.Core.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();

        Task<CreateTodoResponse> AddAsync(TodoItem item);

        Task SaveChangesAsync();

        Task<bool> DeleteTodo(int id);
    }
}