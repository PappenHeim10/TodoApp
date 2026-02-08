using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TodoApp.Core.DTOs;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;
using TodoApp.DataAccess;

namespace TodoApp.Api.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TodosControllers : ControllerBase
    {
        private readonly ILogger<TodosControllers> _logger;
        private readonly ITodoService _todoService;

        public TodosControllers(ILogger<TodosControllers> logger, ITodoService todoService)
        {
            _logger = logger;
            _todoService = todoService;
        }

        [HttpGet]
        [Route("seeAll")]
        public async Task<ActionResult<IEnumerable<GetAllTodosResponse>>> GetTodos()
        {
            IEnumerable<GetAllTodosResponse> response = await _todoService.GetAllTodosAsync();

            if (response.Any())
            {
                _logger.LogInformation("Returning Todos");

                return Ok(response);
            }
            else
            {
                _logger.LogError("No Todos available");

                return Ok(response);
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<CreateTodoResponse>> CreateTodo([FromBody] CreateTodoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CreateTodoResponse response = await _todoService.AddTodoAsync(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            bool delete = await _todoService.DeleteTodoAsync(id);

            if (delete)
            {
                _logger.LogInformation("DeleteTodo method called for ID: {Id} - Todo item deleted successfully", id);

                return NoContent();
            }
            else
            {
                _logger.LogWarning("DeleteTodo method called for ID: {Id} - Todo item not found", id);

                return NotFound();
            }
        }
    }
}