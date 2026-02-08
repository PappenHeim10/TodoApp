using TodoApp.Core.DTOs;
using TodoApp.Core.Entities;

namespace TodoApp.Core.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<GetAllTodosResponse>> GetAllTodosAsync();

        Task<CreateTodoResponse> AddTodoAsync(CreateTodoRequest newTodo);

        Task<bool> DeleteTodoAsync(int id);
    }
}