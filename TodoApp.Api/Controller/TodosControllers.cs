using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Entities;
using TodoApp.DataAccess;

namespace TodoApp.Api.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
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
        [Route("seeAll")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodos()
        {
            _logger.LogInformation("GetTodos method called");

            return await _context.Todos.ToListAsync();
        }


        [HttpPost]
        [Route("createNew")]
        public async Task<ActionResult<TodoItem>> CreateTodo(TodoItem todo)
        {

            _context.Todos.Add(todo);

            _logger.LogInformation("CreateTodo method called with title: {Title}", todo.Title);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodos), new { id = todo.Id }, todo);
        }
    }
}