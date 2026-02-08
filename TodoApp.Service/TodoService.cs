using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoApp.Core.DTOs;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;

namespace TodoApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todorepo;
        private readonly ILogger<TodoService> _logger;

        public TodoService(ITodoRepository todorepo, ILogger<TodoService> logger)
        {
            _todorepo = todorepo;
            _logger = logger;
        }

        public async Task<IEnumerable<GetAllTodosResponse>> GetAllTodosAsync()
        {
            IEnumerable<TodoItem> entities = await _todorepo.GetAllAsync();

            IEnumerable<GetAllTodosResponse> response = entities
                .GroupBy(item => item.UserId)
                .Select(group => new GetAllTodosResponse
                {
                    UserId = group.Key ?? "Anonymous",

                    Todos = group.Select(todo => new TodoDto
                    {
                        Titel = todo.Title,
                        Description = todo.Description,
                        Status = todo.Status
                    }).ToList()
                });

            return response;
        }

        public async Task<CreateTodoResponse> AddTodoAsync(CreateTodoRequest newTodo)
        {
            TodoItem todoItem = new TodoItem
            {
                Title = newTodo.Title,
                Description = newTodo.Description,
                Status = newTodo.Status,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            _logger.LogInformation("Adding new todo item with title: {Title}", newTodo.Title);

            CreateTodoResponse response =  await _todorepo.AddAsync(todoItem);

            if(response.Success)
            {
                _logger.LogInformation("Todo item with title: {Title} added successfully with ID: {Id}", newTodo.Title, response.Id);
                return response;
            }
            else
            {
                _logger.LogWarning("Failed to add todo item with title: {Title}", newTodo.Title);
                return response;
            }
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            _logger.LogInformation("Deleting todo item with ID: {Id}", id);

            bool deleted = await _todorepo.DeleteTodo(id);

            if(deleted)
            {
                _logger.LogInformation("Todo item with ID: {Id} deleted successfully", id);
                return true;
            }
            else
            {
                _logger.LogWarning("Todo item with ID: {Id} not found for deletion", id);
                return false;
            }
        }
    }
}