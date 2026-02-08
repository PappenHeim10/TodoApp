namespace TodoApp.Core.DTOs
{
    public class CreateTodoRequest
    {
        public required string Title { get; set; }

        public required string Description { get; set; }

        public bool Status { get; set; }
    }
}