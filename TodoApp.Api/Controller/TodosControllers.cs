using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Entities;
using TodoApp.DataAccess;

namespace TodoApp.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosControllers : ControllerBase
    {
        private readonly ILogger<TodosControllers> _logger;
        private readonly AppDbContext _context;

        public TodosControllers(ILogger<TodosControllers> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("see all")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        [HttpPost]
        [Route("create new")]
        public async Task<ActionResult<TodoItem>> CreateTodo(TodoItem todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodos), new { id = todo.Id }, todo);
        }
    }
}
