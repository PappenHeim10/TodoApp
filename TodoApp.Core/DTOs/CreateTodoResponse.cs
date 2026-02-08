namespace TodoApp.Core.DTOs
{
    public class CreateTodoResponse
    {
        public int? Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? Created { get; set; } = DateTime.Now;

        public DateTime? Updated { get; set; }

        public bool? Status { get; set; }

        public string? UserId { get; set; }

        public required bool Success { get; set; }
    }
}